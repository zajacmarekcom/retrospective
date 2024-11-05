import useMediaQuery from '@mui/material/useMediaQuery';
import { useTheme } from '@mui/material/styles';
import { Button, Card, CardContent, Typography } from "@mui/material";
import { CheckCircleOutlineRounded } from '@mui/icons-material';


export default function CurrentSprint() {
    const theme = useTheme();
    const isSmallScreen = useMediaQuery(theme.breakpoints.down('sm'));
  
    return (
      <Card sx={{ height: '100%' }}>
        <CardContent>
          <Typography
            component="h2"
            variant="subtitle2"
            gutterBottom
            sx={{ fontWeight: '600' }}
          >
            Current Sprint
          </Typography>
          <Typography sx={{ color: 'text.secondary', mb: '8px' }}>
            Uncover performance and visitor insights with our data wizardry.
          </Typography>
          <Button
            variant="contained"
            size="large"
            color="success"
            endIcon={<CheckCircleOutlineRounded />}
            fullWidth={true}
          >
            Start retrospective!
          </Button>
        </CardContent>
      </Card>
    );
  }