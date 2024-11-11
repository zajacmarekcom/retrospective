import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface TopicCardData {
    id: string,
    category?: TopicsCardType,
    title: string,
    authorName: string,
    authorId: string,
    votes: number,
    userVoted?: boolean
}

export interface TopicsList {
    good: Array<TopicCardData>,
    bad: Array<TopicCardData>,
    improvements: Array<TopicCardData>,
}

export enum TopicsCardType {
    Good,
    Bad,
    Improvements
}

export interface NewTopicDialogState {
    open: boolean,
    category: TopicsCardType | null
}

export interface TopicsState {
    currentTopics: Array<TopicCardData>;
    newTopicDialog: NewTopicDialogState;
}

const initialState: TopicsState = {
    currentTopics: [],
    newTopicDialog: {
        open: false,
        category: null
    }
}

export const topicsSlice = createSlice({
    name: 'topics',
    initialState,
    reducers: {
        openNewTopicDialog: (state, action: PayloadAction<TopicsCardType>) => {
            state.newTopicDialog.open = true;
            state.newTopicDialog.category = action.payload;
        },
        closeNewTopicDialog: (state) => {
            state.newTopicDialog.open = false;
            state.newTopicDialog.category = null;
        },
        addTopic: (state, action: PayloadAction<{title: string, category: TopicsCardType}>) => {
            const newTopic: TopicCardData = {
                id: Math.random().toString(36).substr(2, 9),
                category: action.payload.category,
                title: action.payload.title,
                authorName: 'Anonymous',
                authorId: '0',
                votes: 0
            }
            state.currentTopics.push(newTopic);
        },
        voteTopic: (state, action: PayloadAction<{topicId: string, category: TopicsCardType}>) => {
            let topic = state.currentTopics.find(topic => topic.id === action.payload.topicId && topic.category === action.payload.category);

            if (topic && !topic.userVoted) {
                topic.votes++;
                topic.userVoted = true;
            }
        },

        unvoteTopic: (state, action: PayloadAction<{topicId: string, category: TopicsCardType}>) => {
            let topic = state.currentTopics.find(topic => topic.id === action.payload.topicId && topic.category === action.payload.category);

            if (topic && topic.userVoted) {
                topic.votes--
                topic.userVoted = false;
            }
        },
    }
});

export const { openNewTopicDialog, closeNewTopicDialog, addTopic, voteTopic, unvoteTopic } = topicsSlice.actions;
export default topicsSlice.reducer;