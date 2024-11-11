import { Alert, AlertTitle, Backdrop, CircularProgress, Snackbar, Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { closeNotification } from '../sessionSlice';

export interface SessionNotificationProps {
    sessionId: string;
}

export default function SessionNotification(props: SessionNotificationProps) {
    let [reminingTime, setRemainingTime] = useState(5);
    const navigate = useNavigate();
    const dispatch = useDispatch();

    useEffect(() => {
        function tick() {
            setRemainingTime((prev) => prev - 1);
        }

        const countdown = setInterval(tick, 1000);

        if (reminingTime === 0) {
            clearInterval(countdown);
            dispatch(closeNotification());
            navigate(`/app/session/${props.sessionId}`);
        }

        return () => clearInterval(countdown);
    }, [reminingTime]);

    return (
        <>
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
        </>)
}