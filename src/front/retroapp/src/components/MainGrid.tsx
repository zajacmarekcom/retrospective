import Grid from '@mui/material/Grid2';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import CurrentSprint from './CurrentSprint';
import TopicsCard from './TopicsCard';
import { TopicsCardType } from '../features/topics/topicSlice';

export default function MainGrid() {

  return (
    <Box sx={{ width: '100%', maxWidth: { sm: '100%', md: '1700px' }}}>
      <Grid
        container
        spacing={2}
        columns={12}
        sx={{ mb: (theme) => theme.spacing(2) }}
      >
        <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
          <CurrentSprint />
        </Grid>
      </Grid>
      <Typography component="h2" variant="h6" sx={{ mb: 2 }}>
        Topics for the current Sprint
      </Typography>
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
    </Box>
  );
}
