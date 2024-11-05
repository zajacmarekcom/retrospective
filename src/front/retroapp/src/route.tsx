import { createBrowserRouter } from "react-router-dom";
import App from "./pages/App";
import LoggedInLayout from "./layouts/LoggedInLayout";
import Dashboard from "./pages/Dashboard";
import Session from "./pages/Session";

const route = createBrowserRouter([
    {
        path: "",
        element: <App />,
        children: [
            {
                path: "app",
                element: <LoggedInLayout />,
                children: [
                    {
                        path: "",
                        element: <Dashboard />
                    },
                    {
                        path: "session/:id",
                        element: <Session />
                    }
                ]
            }
        ]
    }
]);

export default route;