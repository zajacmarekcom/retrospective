import { CssBaseline, Box, Stack } from "@mui/material";
import { Outlet } from "react-router-dom";
import AppTheme from "../shared-theme/AppTheme";
import AppNavbar from '../components/AppNavbar';
import SideMenu from '../components/SideMenu';
import { chartsCustomizations, treeViewCustomizations } from "../theme/customizations";
import Header from "../components/Header";

function LoggedInLayout() {
    const xThemeComponents = {
        ...chartsCustomizations,
        ...treeViewCustomizations,
    };

    return (
        <AppTheme themeComponents={xThemeComponents}>
            <CssBaseline enableColorScheme />
            <Box sx={{ display: 'flex' }}>
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
            </Box>
        </AppTheme>
    )
}

export default LoggedInLayout
