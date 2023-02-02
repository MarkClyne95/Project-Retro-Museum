use rand::distributions::{Distribution, Uniform}; // 0.6.5

#[no_mangle]
pub extern "C" fn add_sum(a: i32, b: i32) -> i32 {
    return a + b;
}

#[no_mangle]
pub extern "C" fn double_num(a: i32) -> i32 {
    return a * 2;
}

#[no_mangle]
pub extern "C" fn triple_num(a: i32) -> i32 {
    return a * 3;
}

#[no_mangle]
pub extern "C" fn rnd_num(upper_range: i32, lower_range: i32) -> i32 {
    let step = Uniform::new(lower_range, upper_range);
    let mut rng = rand::thread_rng();
    let choice = step.sample(&mut rng);
    return choice;
}
