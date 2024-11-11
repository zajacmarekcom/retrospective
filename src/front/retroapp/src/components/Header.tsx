import Stack from '@mui/material/Stack';
import NotificationsRoundedIcon from '@mui/icons-material/NotificationsRounded';
import MenuButton from './MenuButton';
import ColorModeIconDropdown from '../shared-theme/ColorModeIconDropdown';
import { Typography } from '@mui/material';
import InterpreterModeOutlinedIcon from '@mui/icons-material/InterpreterModeOutlined';

export default function Header() {
  return (
    <Stack
      direction="row"
      sx={{
        display: { xs: 'none', md: 'flex' },
        width: '100%',
        alignItems: { xs: 'flex-start', md: 'center' },
        justifyContent: 'space-between',
        maxWidth: { sm: '100%', md: '1700px' },
        pt: 1.5,
      }}
      spacing={2}
    >
      <Stack direction="row" alignItems="center" gap={1}>
        <InterpreterModeOutlinedIcon />
        <Typography component='h1' variant='h4' sx={{ letterSpacing: '0.5rem' }}>
          Retrospective.dev
        </Typography>
      </Stack>

      <Stack direction="row" sx={{ gap: 1 }}>
        <MenuButton showBadge aria-label="Open notifications">
          <NotificationsRoundedIcon />
        </MenuButton>
        <ColorModeIconDropdown />
      </Stack>
    </Stack>
  );
}
