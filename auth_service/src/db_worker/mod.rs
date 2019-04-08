use std::sync::Arc;
use time::Duration;

use crate::state::AppState;
use std::fs::OpenOptions;
use timer::{Timer, Guard};
use std::io::Write;

pub fn start_new(state: Arc<AppState>) -> Guard {
    let timer = Timer::new();
    timer.schedule_repeating(Duration::seconds(5), move || {
        let is_modified = state.is_modified();
        if !is_modified { return; }

        let result = OpenOptions::new().write(true).open(state.file_path.clone());
        if let Ok(mut file) = result {
            println!("Writting file");
            for user in state.users.read().unwrap().values() {
                let serialized: String = String::from(user);
                let result = file.write(serialized.as_bytes());
                if let Err(e) = result {
                    println!("{}", e);
                }
            }
            let result = file.flush();
            if let Err(e) = result {
                println!("{}", e);
            }
        }
        state.finish_write();
    })
}