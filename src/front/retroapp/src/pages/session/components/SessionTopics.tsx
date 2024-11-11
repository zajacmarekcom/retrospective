import Grid from "@mui/material/Grid2";
import TopicsCard from "../../../components/TopicsCard";
import { TopicsCardType } from "../../../features/topics/topicSlice";

export default function SessionTopics() {
  return (
    <Grid container spacing={2} columns={12}>
      <Grid size={{ xs: 12, lg: 4 }}>
        <TopicsCard type={TopicsCardType.Good} allowEdit={true} />
      </Grid>
      <Grid size={{ xs: 12, lg: 4 }}>
        <TopicsCard type={TopicsCardType.Bad} allowEdit={true} />
      </Grid>
      <Grid size={{ xs: 12, lg: 4 }}>
        <TopicsCard type={TopicsCardType.Improvements} allowEdit={true} />
      </Grid>
    </Grid>
  );
}
