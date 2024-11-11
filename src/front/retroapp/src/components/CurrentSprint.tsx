import { Button, Card, CardContent, Typography } from "@mui/material";
import { CheckCircleOutlineRounded } from '@mui/icons-material';
import { useDispatch } from 'react-redux';
import { startSession } from '../features/session/sessionSlice';


export default function CurrentSprint() {
    const dispatch = useDispatch();

    function startNewSession() {
      dispatch(startSession('1'));
    }
  
    return (
      <Card sx={{ height: '100%' }}>
        <CardContent>
          <Typography
            component="h2"
            variant="subtitle2"
            gutterBottom
            sx={{ fontWeight: '600' }}
          >
            Go with it!
          </Typography>
          <Typography sx={{ color: 'text.secondary', mb: '8px' }}>
            <b>Ready for discuss</b> topics from the latest Sprint? <b>Start Session now</b> and become the Host of the Retrospective!
          </Typography>
          <Button
            variant="contained"
            size="large"
            color="success"
            endIcon={<CheckCircleOutlineRounded />}
            fullWidth={true}
            onClick={startNewSession}
          >
            Start retrospective!
          </Button>
        </CardContent>
      </Card>
    );
  }