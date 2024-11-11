import { Outlet } from "react-router-dom";
import { CssBaseline, Box } from "@mui/material";
import { chartsCustomizations, treeViewCustomizations } from "../theme/customizations";
import AppTheme from "../shared-theme/AppTheme";

function App() {
  const xThemeComponents = {
    ...chartsCustomizations,
    ...treeViewCustomizations,
};

  return (
    <AppTheme themeComponents={xThemeComponents}>
            <CssBaseline enableColorScheme />
            <Box sx={{ display: 'flex' }}>
              <Outlet />
            </Box>
    </AppTheme>
  )
}

export default App
