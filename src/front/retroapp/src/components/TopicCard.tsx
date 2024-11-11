import {
  Card,
  CardContent,
  IconButton,
  Stack,
  Typography,
} from "@mui/material";
import {
  TopicCardData,
  unvoteTopic,
  voteTopic,
} from "../features/topics/topicSlice";
import ThumbUpIcon from "@mui/icons-material/ThumbUp";
import ThumbUpOutlinedIcon from "@mui/icons-material/ThumbUpOutlined";
import DeleteIcon from "@mui/icons-material/Delete";
import Grid from "@mui/material/Grid2";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store/store";

export default function TopicCard(props: { id: string; allowEdit: boolean }) {
  const dispatch = useDispatch();

  const topic = useSelector((state: RootState) =>
    state.topics.currentTopics.find(
      (topic: TopicCardData) => topic.id === props.id
    )
  );

  function vote() {
    console.log("vote");
    dispatch(voteTopic({ topicId: props.id, category: topic!.category! }));
  }

  function unvote() {
    console.log("unvote");
    dispatch(unvoteTopic({ topicId: props.id, category: topic!.category! }));
  }

  return (
    <Card
      sx={{
        mt: 2,
        width: "100%",
        border: "1px solid",
        borderColor: (theme) => theme.palette.info.main,
      }}
    >
      <CardContent sx={{ display: "flex", justifyContent: "center", my: 0 }}>
        <Stack
          direction="column"
          sx={{
            justifyContent: "center",
            alignItems: "stretch",
            width: "100%",
          }}
        >
          <Grid container spacing={2} columns={12}>
            <Grid size={{ xs: 12 }}>
              <Typography variant="subtitle1">{topic!.title}</Typography>
            </Grid>
          </Grid>
          <Grid
            container
            spacing={2}
            columns={12}
            alignContent="space-between"
            alignItems={"center"}
          >
            <Grid size={{ xs: 8 }}>
              <Typography
                variant="caption"
                sx={{ color: (theme) => theme.palette.text.disabled }}
              >
                Votes: {topic!.votes}
              </Typography>
            </Grid>
            <Grid size={{ xs: 4 }} sx={{ textAlign: "end" }}>
              {props.allowEdit && (
                <IconButton sx={{ mr: 2 }}>
                  <DeleteIcon color="error" />
                </IconButton>
              )}
              {topic!.userVoted && (
                <IconButton onClick={unvote}>
                  {topic!.userVoted && <ThumbUpIcon color="info" />}
                </IconButton>
              )}
              {!topic!.userVoted && (
                <IconButton onClick={vote}>
                  {!topic!.userVoted && (
                    <ThumbUpOutlinedIcon color="disabled" />
                  )}
                </IconButton>
              )}
            </Grid>
          </Grid>
        </Stack>
      </CardContent>
    </Card>
  );
}
