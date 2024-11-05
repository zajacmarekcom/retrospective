import Grid from '@mui/material/Grid2';
import Box from '@mui/material/Box';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import Copyright from '../internals/components/Copyright';
import CustomizedTreeView from './CustomizedTreeView';
import PageViewsBarChart from './PageViewsBarChart';
import SessionsChart from './SessionsChart';
import StatCard, { StatCardProps } from './StatCard';
import CurrentSprint from './CurrentSprint';
import { Alert, AlertTitle, Backdrop, CircularProgress, Snackbar } from '@mui/material';
import { useEffect, useState } from 'react';
import { redirect, useNavigate } from 'react-router-dom';

const data: StatCardProps[] = [
  {
    title: 'Users',
    value: '14k',
    interval: 'Last 30 days',
    trend: 'up',
    data: [
      200, 24, 220, 260, 240, 380, 100, 240, 280, 240, 300, 340, 320, 360, 340, 380,
      360, 400, 380, 420, 400, 640, 340, 460, 440, 480, 460, 600, 880, 920,
    ],
  },
  {
    title: 'Conversions',
    value: '325',
    interval: 'Last 30 days',
    trend: 'down',
    data: [
      1640, 1250, 970, 1130, 1050, 900, 720, 1080, 900, 450, 920, 820, 840, 600, 820,
      780, 800, 760, 380, 740, 660, 620, 840, 500, 520, 480, 400, 360, 300, 220,
    ],
  },
  {
    title: 'Event count',
    value: '200k',
    interval: 'Last 30 days',
    trend: 'neutral',
    data: [
      500, 400, 510, 530, 520, 600, 530, 520, 510, 730, 520, 510, 530, 620, 510, 530,
      520, 410, 530, 520, 610, 530, 520, 610, 530, 420, 510, 430, 520, 510,
    ],
  },
];

export default function MainGrid() {
  let [reminingTime, setRemainingTime] = useState(5);
  const navigate = useNavigate();

  useEffect(() => {
    function tick() {
      setRemainingTime((prev) => prev - 1);
    }

    const countdown = setInterval(tick, 1000);

    if (reminingTime === 0) {
      clearInterval(countdown);
      navigate('session/1');
    }

    return () => clearInterval(countdown);
  }, [reminingTime]);

  return (
    <Box sx={{ width: '100%', maxWidth: { sm: '100%', md: '1700px' } }}>
      <Backdrop 
      sx={(theme) => ({ color: '#fff', zIndex: theme.zIndex.drawer + 1 })}
      open
      >
        <CircularProgress color="inherit" />
      </Backdrop>
      <Snackbar open anchorOrigin={{ vertical: 'top', horizontal: 'center' }}>
        <Alert variant="filled" severity="success" sx={{ mb: 2 }}>
          <AlertTitle>
          <Typography component="h4" variant="h4" sx={{ mb: 2 }}>
          Retrospective session started!
      </Typography>
          </AlertTitle>
          You'll be redirected to the session in {reminingTime} seconds...
        </Alert>
      </Snackbar>
      
      {/* cards */}
      <Typography component="h2" variant="h6" sx={{ mb: 2 }}>
        Overview
      </Typography>
      <Grid
        container
        spacing={2}
        columns={12}
        sx={{ mb: (theme) => theme.spacing(2) }}
      >
        <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
          <CurrentSprint />
        </Grid>
        {data.map((card, index) => (
          <Grid key={index} size={{ xs: 12, sm: 6, lg: 3 }}>
            <StatCard {...card} />
          </Grid>
        ))}
        <Grid size={{ xs: 12, md: 4 }}>
          <SessionsChart />
        </Grid>
        <Grid size={{ xs: 12, md: 8 }}>
          <PageViewsBarChart />
        </Grid>
      </Grid>
      <Typography component="h2" variant="h6" sx={{ mb: 2 }}>
        Details
      </Typography>
      <Grid container spacing={2} columns={12}>
        <Grid size={{ xs: 12, lg: 9 }}>
        </Grid>
        <Grid size={{ xs: 12, lg: 3 }}>
          <Stack gap={2} direction={{ xs: 'column', sm: 'row', lg: 'column' }}>
            <CustomizedTreeView />
          </Stack>
        </Grid>
      </Grid>
      <Copyright sx={{ my: 4 }} />
    </Box>
  );
}
