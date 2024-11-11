import { Button } from "@mui/material"
import { Link } from "react-router-dom"

function Landing() {
  return (
    <Button
    variant="contained"
    size="small"
    color="primary"
    component={Link} to="/app">
        Go to Dashboard
    </Button>
  )
}

export default Landing
