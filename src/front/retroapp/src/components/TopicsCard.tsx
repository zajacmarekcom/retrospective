import { Button, Card, CardContent, Stack, Typography } from "@mui/material";
import AddCircleOutlineIcon from "@mui/icons-material/AddCircleOutline";
import EmptyTopicCard from "./EmptyTopicCard";
import { TopicCardData, TopicsCardType } from "../features/topics/topicSlice";
import { useDispatch, useSelector } from "react-redux";
import { openNewTopicDialog } from "../features/topics/topicSlice";
import { RootState } from "../store/store";
import TopicCard from "./TopicCard";
import { createSelector } from "@reduxjs/toolkit";

export interface TopicsCardProps {
  type: TopicsCardType;
  allowEdit: boolean;
}

export default function TopicsCard(props: TopicsCardProps) {
  const dispatch = useDispatch();
  function openAddTopicDialog() {
    dispatch(openNewTopicDialog(props.type));
  }

  const topics = (state: RootState) => state.topics.currentTopics;
  const topicsSelector = createSelector([topics], (topics) =>
    topics.filter((topic: TopicCardData) => topic.category == props.type)
  );
  const currentTopics = useSelector(topicsSelector);

  function mapTitle(type: TopicsCardType) {
    switch (type) {
      case TopicsCardType.Good:
        return "Good";
      case TopicsCardType.Bad:
        return "Not good";
      case TopicsCardType.Improvements:
        return "What can be improved?";
      default:
        return "";
    }
  }

  return (
    <Card sx={{ height: "100%" }}>
      <CardContent>
        <Typography
          component="h3"
          variant="h5"
          align="center"
          gutterBottom
          sx={{ fontWeight: "600" }}
        >
          {mapTitle(props.type)}
        </Typography>
        {props.allowEdit && (
          <Button
            variant="outlined"
            size="medium"
            color="secondary"
            fullWidth={true}
            onClick={openAddTopicDialog}
          >
            <Stack direction="row" alignItems="center" gap={1}>
              <AddCircleOutlineIcon />
              <Typography component="span" variant="subtitle2">
                Add topic
              </Typography>
            </Stack>
          </Button>
        )}
        <Stack direction="column" alignItems="center">
          {currentTopics.length == 0 && <EmptyTopicCard />}
          {currentTopics.map((topic) => (
            <TopicCard key={topic.id} id={topic.id} allowEdit={props.allowEdit} />
          ))}
        </Stack>
      </CardContent>
    </Card>
  );
}
