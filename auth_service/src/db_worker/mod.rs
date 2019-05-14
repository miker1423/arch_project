use std::sync::Arc;

use crate::state::AppState;
use std::fs::OpenOptions;
use std::io::Write;
use tokio::timer::Interval;
use std::time::Duration;
use tokio::prelude::*;

pub fn start_new(state: Arc<AppState>) {
    let stream = Interval::new_interval(Duration::from_secs(5))
        .for_each(move |_| {
            let is_modified = state.is_modified();
            if !is_modified { return Ok(()); }

            let result = OpenOptions::new().write(true).truncate(true).open(state.file_path.clone());
            if let Ok(mut file) = result {
                for user in state.users.read().unwrap().values() {
                    dbg!(user);
                    let serialized: String = String::from(user);
                    let result = file.write(serialized.as_bytes());
                    if result.is_err() { }
                }
                let result = file.flush();
                if result.is_err() { }
            }
            state.finish_write();
            Ok(())
    }).map_err(|e| println!("interval error, err:{:?}", e));
    tokio::run(stream);
}