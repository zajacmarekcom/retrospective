import { Card, CardContent, Stack, Typography } from "@mui/material";
import ContentPasteOffIcon from '@mui/icons-material/ContentPasteOff';

export default function EmptyTopicCard() {
    return (
        <Card sx={{ mt: 2, width: '100%', border: '2px dotted', borderColor: ((theme) => theme.palette.info.main), color: ((theme) => theme.palette.text.disabled) }}>
            <CardContent sx={{ display: 'flex', justifyContent: 'center', my: 1 }}>
                <Stack direction="column" alignItems="center">
                    <Stack direction="row" alignItems="center" gap={1}>
                        <ContentPasteOffIcon />
                        <Typography variant='subtitle2'>
                            No topics
                        </Typography>
                    </Stack>
                    <Typography variant='caption'>
                        Add first topic to the current sprint
                    </Typography>
                </Stack>
            </CardContent>
        </Card>
    )
}