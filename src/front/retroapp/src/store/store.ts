import { configureStore } from '@reduxjs/toolkit';
import sessionReducer from '../features/session/sessionSlice';
import topicsReducer from '../features/topics/topicSlice';

export const store = configureStore({
    reducer: {
        session: sessionReducer,
        topics: topicsReducer
    },
    });

export type RootState = ReturnType<typeof store.getState>;

export type AppDispatch = typeof store.dispatch;