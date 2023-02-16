namespace ScottEwing{
    public class Ref<T>{
        private T _value;

        public T Value {
            get => _value;
            set => _value = value;
        }

        public Ref(T reference) {
            Value = reference;
        }
    }
}
