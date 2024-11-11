import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../../store/store";
import { useBeforeUnload } from "react-router-dom";
import {
  userEnteredSession,
  userLeftSession,
  SessionState,
  SessionStatus,
  goNext,
} from "../../features/session/sessionSlice";
import SessionTopics from "./components/SessionTopics";
import NavigateNextIcon from "@mui/icons-material/NavigateNext";
import { Box, Button, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import SessionVoting from "./components/SessionVoting";
import { useEffect, useState } from "react";

export default function Session() {
  const dispatch = useDispatch();
  const sessionState = useSelector((state: RootState) => state.session);

  const [seconds, setSeconds] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setSeconds((seconds) => seconds + 1);
    }, 1000);
    return () => clearInterval(interval);
  }, []);

  function getTime(time: number): string {
    // hours, minutes and seconds
    const hrs = Math.floor(time / 3600);
    const mins = Math.floor((time % 3600) / 60);
    const secs = time % 60;

    // "0" padding
    const minsStr = mins < 10 ? "0" + mins : mins;
    const secsStr = secs < 10 ? "0" + secs : secs;

    return `${hrs}:${minsStr}:${secsStr}`;
  }

  function stateView(state: SessionState) {
    switch (state.status) {
      case SessionStatus.Topics:
        return <SessionTopics />;
      case SessionStatus.Voting:
        return <SessionVoting />;
      default:
        return <div></div>;
    }
  }

  dispatch(userEnteredSession());

  useBeforeUnload(() => {
    dispatch(userLeftSession());
  });

  return (
    <>
      <Box
        sx={{ width: "100%", py: 5, maxWidth: { sm: "100%", md: "1700px" } }}
      >
        <Grid
          container
          spacing={2}
          columns={12}
          justifyContent="space-between"
          sx={{ mb: (theme) => theme.spacing(2) }}
        >
          <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
            <Button
              onClick={() => dispatch(goNext())}
              variant="contained"
              color="success"
              size="large"
              startIcon={<NavigateNextIcon />}
            >
              Finish this step
            </Button>
          </Grid>
          <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
            <Typography variant="h4" component="span">
              Session duration: {getTime(seconds)}
            </Typography>
          </Grid>
        </Grid>
      </Box>
      <Box sx={{ width: "80%", maxWidth: { sm: "80%", md: "1500px" } }}>
        {stateView(sessionState)}
      </Box>
    </>
  );
}
