import { Button } from "@mui/material";
import { Link } from "react-router-dom";

export interface GoToSessionProps {
    isSessionStarted: boolean;
    sessionId: string | null;
}

export default function GoToSessionButton(props: GoToSessionProps) {
    return (
        <Button
        disabled={!props.isSessionStarted}
            variant="contained"
            size="medium"
            color="success"
            fullWidth={true}
            component={Link} to={`/app/session/${props.sessionId}`}>
            Go to Retro Session
        </Button>
    )
}