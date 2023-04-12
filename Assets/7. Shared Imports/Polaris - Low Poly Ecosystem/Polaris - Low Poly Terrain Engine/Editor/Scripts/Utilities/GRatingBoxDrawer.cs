#if GRIFFIN
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using System.Text;
using System;

namespace Pinwheel.Griffin
{
    public class GRatingBoxDrawer
    {
        private const int STAGE_STARS = 0;
        private const int STAGE_FEEDBACK = 1;
        private const int STAGE_RATE_ON_STORE = 2;

        private int m_stageId;
        protected IMGUIContainer m_imgui;

        private int m_rating = 0;
        private string m_feedback = "";

        protected const string VERSION = "4";
        protected const string PREF_HIDE_COUNT = "polaris-rating-hide-count";
        protected const string PREF_HIDE_DATE = "polaris-rating-hide-date";

        internal bool isHidden;

        public GRatingBoxDrawer()
        {
            m_stageId = STAGE_STARS;
        }

        public void DrawGUI()
        {
            if (isHidden)
                return;

            GenericMenu menu = new GenericMenu();
            menu.AddItem(StarsGUI.HIDE, false, Hide);
            if (HasHiddenBefore())
            {
                menu.AddItem(StarsGUI.DONT_SHOW_AGAIN, false, HideDontShowAgain);
            }
            GEditorCommon.Foldout("Feedback", true, "polaris-rating-box", () =>
             {
                 if (m_stageId == STAGE_STARS)
                 {
                     DrawStageStars();
                 }
                 else if (m_stageId == STAGE_FEEDBACK)
                 {
                     DrawStageFeedback();
                 }
                 else if (m_stageId == STAGE_RATE_ON_STORE)
                 {
                     DrawStageRateOnStore();
                 }
             },
             menu);
        }

        private class StarsGUI
        {
            public static readonly GUIContent HIDE = new GUIContent("Hide");
            public static readonly GUIContent DONT_SHOW_AGAIN = new GUIContent("Don't show again");

            public static readonly GUIContent HOW_YOU_RATE = new GUIContent("       How is your experience with Polaris?");
            public static readonly Color32 GRAY = new Color32(128, 128, 128, 255);
            public static readonly Color32 YELLOW = new Color32(255, 220, 75, 255);
            public static readonly string[] STAR_LABELS = new string[]
            {
                "Not helpful at all, even cause me trouble",
                "Barely work, has many errors but not critical",
                "Can do the work, but lack of features or hard to use",
                "Good, but need some improvements",
                "Excellent, it can do almost everything I needed"
            };

            private static Texture2D s_starTexture;
            public static Texture2D starTexture
            {
                get
                {
                    if (s_starTexture == null)
                    {
                        s_starTexture = GEditorSkin.Instance.GetTexture("Star");
                    }
                    return s_starTexture;
                }
            }
        }

        private void DrawStageStars()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(StarsGUI.HOW_YOU_RATE, GEditorCommon.CenteredLabel);
            //if (EditorCommon.ContextButton())
            //{
            //    GenericMenu menu = new GenericMenu();
            //    menu.AddItem(StarsGUI.HIDE, false, Hide);
            //    if (HasHiddenBefore())
            //    {
            //        menu.AddItem(StarsGUI.DONT_SHOW_AGAIN, false, HideDontShowAgain);
            //    }
            //    menu.ShowAsContext();
            //}
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            RectOffset offset = new RectOffset(3, 3, 3, 3);
            int score = 0;
            Rect[] rects = new Rect[5];
            for (int i = 0; i < rects.Length; ++i)
            {
                Rect r = EditorGUILayout.GetControlRect(GUILayout.Width(36), GUILayout.Height(36));
                rects[i] = r;
                if (offset.Add(r).Contains(Event.current.mousePosition))
                {
                    score = i + 1;
                }
            }

            for (int i = 0; i < rects.Length; ++i)
            {
                GUI.color = i < score ? StarsGUI.YELLOW : StarsGUI.GRAY;
                GUI.DrawTexture(rects[i], StarsGUI.starTexture);
            }
            GUI.color = Color.white;

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < rects.Length; ++i)
            {
                if (offset.Add(rects[i]).Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseUp)
                {
                    m_rating = i + 1;
                    if (m_rating == 5)
                    {
                        m_stageId = STAGE_RATE_ON_STORE;
                    }
                    else
                    {
                        m_stageId = STAGE_FEEDBACK;
                    }
                }
            }
        }

        private class FeedbackGUI
        {
            public static readonly GUIContent CHANGE_RATING = new GUIContent("Change");
            public static readonly GUIContent FEEDBACK_LABEL = new GUIContent("Please share your feedback and suggestion:");
            public static readonly GUIContent SEND_EMAIL = new GUIContent("Send via Email →");
            public static readonly string EMAIL_SUBJECT = "[POLARIS] Rating and Feedback";

            public static string GetRatingText(int rating)
            {
                return $"Your rating: {rating}/5";
            }

            public static string GetEmailBody(int rating, string feedback)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Rating: {rating}/5");
                sb.AppendLine("Feedback:");
                sb.Append(feedback);
                return UnityEngine.Networking.UnityWebRequest.EscapeURL(sb.ToString());
            }
        }

        private void DrawStageFeedback()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(FeedbackGUI.GetRatingText(m_rating), EditorStyles.boldLabel, GUILayout.Width(110));
            if (GUILayout.Button(FeedbackGUI.CHANGE_RATING, EditorStyles.miniButton, GUILayout.Width(70)))
            {
                m_stageId = STAGE_STARS;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField(FeedbackGUI.FEEDBACK_LABEL);
            m_feedback = EditorGUILayout.TextArea(m_feedback, GUILayout.Height(100));
            if (GUILayout.Button(FeedbackGUI.SEND_EMAIL))
            {
                Hide();
                GEditorCommon.OpenEmailEditor(
                    GCommon.SUPPORT_EMAIL,
                    FeedbackGUI.EMAIL_SUBJECT,
                    FeedbackGUI.GetEmailBody(m_rating, m_feedback));
            }
        }

        private class RateOnStoreGUI
        {
            public static readonly GUIContent WOULD_YOU_LIKE = new GUIContent("Would you like to leave a 5-stars review on the Asset Store?");
            public static readonly GUIContent YES = new GUIContent("Yes, take me there →");
            public static readonly GUIContent NO = new GUIContent("May be later");
            public static readonly string REVIEW_LINK = $"https://assetstore.unity.com/packages/slug/170400#review";

            public static readonly Color32 BLUE = new Color32(0, 200, 255, 255);
            public static readonly Color32 GRAY = new Color32(180, 180, 180, 255);

            public static string GetRatingText(int rating)
            {
                return $"Your rating: {rating}/5";
            }
        }

        private void DrawStageRateOnStore()
        {
            EditorGUILayout.LabelField(RateOnStoreGUI.GetRatingText(m_rating), EditorStyles.boldLabel);
            EditorGUILayout.LabelField(RateOnStoreGUI.WOULD_YOU_LIKE);
            GUI.backgroundColor = RateOnStoreGUI.BLUE;
            if (GUILayout.Button(RateOnStoreGUI.YES))
            {
                HideDontShowAgain();
                Application.OpenURL(RateOnStoreGUI.REVIEW_LINK);
            }
            GUI.backgroundColor = Color.white;
            GUI.color = RateOnStoreGUI.GRAY;
            if (GUILayout.Button(RateOnStoreGUI.NO))
            {
                Hide();
            }
            GUI.color = Color.white;
        }

        public void Hide()
        {
            MarkHide();
            isHidden = true;
            //this.RemoveFromHierarchy();
        }

        private static void MarkHide()
        {
            int hideCount = EditorPrefs.GetInt(PREF_HIDE_COUNT + VERSION, 0);
            hideCount += 1;
            EditorPrefs.SetInt(PREF_HIDE_COUNT + VERSION, hideCount);

            DateTime timeNow = DateTime.Now;
            EditorPrefs.SetString(PREF_HIDE_DATE + VERSION, timeNow.ToString());
        }

        public void HideDontShowAgain()
        {
            MarkDontShowAgain();
            isHidden = true;
            //this.RemoveFromHierarchy();
        }

        private static void MarkDontShowAgain()
        {
            int hideCount = EditorPrefs.GetInt(PREF_HIDE_COUNT + VERSION, 0);
            hideCount += 1;
            EditorPrefs.SetInt(PREF_HIDE_COUNT + VERSION, hideCount);

            DateTime timeFar = new DateTime(3000, 1, 1, 0, 0, 0, 0);
            EditorPrefs.SetString(PREF_HIDE_DATE + VERSION, timeFar.ToString());
        }

        protected bool HasHiddenBefore()
        {
            int hideCount = EditorPrefs.GetInt(PREF_HIDE_COUNT + VERSION, 0);
            return hideCount > 0;
        }

        public static bool IsAppropriateForShowing()
        {
            int hideCount = EditorPrefs.GetInt(PREF_HIDE_COUNT + VERSION, 0);
            if (hideCount == 0) //never shown before, need to wait for another 7 days
            {
                MarkHide();
                return false;
            }
            else
            {
                DateTime lastHiddenDate;
                string timeString = EditorPrefs.GetString(PREF_HIDE_DATE + VERSION, string.Empty);
                if (!DateTime.TryParse(timeString, out lastHiddenDate))
                {
                    return true;
                }
                else
                {
                    DateTime now = DateTime.Now;
                    if (now > lastHiddenDate)
                    {
                        TimeSpan span = now.Subtract(lastHiddenDate);
                        double total = span.TotalDays;
                        bool canShow = total > 7;
                        return canShow;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
#endif