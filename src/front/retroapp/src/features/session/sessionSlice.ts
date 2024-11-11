import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'

export enum SessionStatus {
    Topics,
    Voting,
    Review,
    Summary
}

export interface SessionState {
    sessionStarted: boolean,
    sessionId: string | null,
    status: SessionStatus,
    isUserOnSession: boolean,
    notificationWasShown: boolean,
    showNotification: boolean,
}

const initialState: SessionState = {
    sessionStarted: false,
    sessionId: null,
    status: SessionStatus.Topics,
    isUserOnSession: false,
    notificationWasShown: false,
    showNotification: false,
}

export const sessionSlice = createSlice({
    name: 'session',
    initialState,
    reducers: {
        startSession: (state, action: PayloadAction<string>) => {
            state.sessionStarted = true
            state.sessionId = action.payload
        },
        endSession: (state) => {
            state.sessionStarted = false
            state.sessionId = null
            state.isUserOnSession = false
            state.notificationWasShown = false
        },
        userEnteredSession: (state) => {
            state.isUserOnSession = true
        },
        userLeftSession: (state) => {
            state.isUserOnSession = false
        },
        notifyUser: (state) => {
            if (!state.notificationWasShown) {
                state.notificationWasShown = true;
                state.showNotification = true;
            }
        },
        closeNotification: (state) => {
            state.showNotification = false;
        },
        goNext: (state) => {
            switch (state.status) {
                case SessionStatus.Topics:
                    state.status = SessionStatus.Voting;
                    break;
                case SessionStatus.Voting:
                    state.status = SessionStatus.Review;
                    break;
                case SessionStatus.Review:
                    state.status = SessionStatus.Summary;
            }
        }
    },
})

export const { startSession, endSession, notifyUser, closeNotification, userEnteredSession, userLeftSession, goNext } = sessionSlice.actions;

export default sessionSlice.reducer;