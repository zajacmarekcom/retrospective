import { Box, Stack } from "@mui/material";
import { Outlet } from "react-router-dom";
import AppNavbar from '../components/AppNavbar';
import SideMenu from '../components/SideMenu';
import Header from "../components/Header";
import SessionNotification from "../features/session/components/SessionNotification";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store/store";
import { notifyUser } from "../features/session/sessionSlice";
import NewTopicDialog from "../components/NewTopicDialog";

function LoggedInLayout() {
    const sessionStarted = useSelector((state: RootState) => state.session.sessionStarted);
    const sessionId = useSelector((state: RootState) => state.session.sessionId);
    const showNotification = useSelector((state: RootState) => state.session.showNotification);
    const dispatch = useDispatch();

    useEffect(() => {
        if (sessionStarted) {
            dispatch(notifyUser());
        }
    }, [sessionStarted]);

    return (
        <>
            <SideMenu />
            <AppNavbar />
            <Box
                component="main"
                sx={(theme) => ({
                    flexGrow: 1,
                    backgroundColor: theme.vars
                        ? `rgba(${theme.vars.palette.background.defaultChannel} / 1)`
                        : alpha(theme.palette.background.default, 1),
                    overflow: 'auto',
                })}
            >
                {showNotification && <SessionNotification sessionId={sessionId!} />}
                <Stack
                    spacing={2}
                    sx={{
                        alignItems: 'center',
                        mx: 3,
                        pb: 5,
                        mt: { xs: 8, md: 0 },
                    }}
                >
                    <Header />
                    <Outlet />
                </Stack>
            </Box>
            <NewTopicDialog />
        </>
    )
}

export default LoggedInLayout
