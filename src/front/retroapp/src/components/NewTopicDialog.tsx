import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField, Typography } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import { addTopic, closeNewTopicDialog, TopicsCardType } from "../features/topics/topicSlice";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store/store";

export default function NewTopicDialog() {
    const open = useSelector((state: RootState) => state.topics.newTopicDialog.open);
    const category = useSelector((state: RootState) => state.topics.newTopicDialog.category);
    const dispatch = useDispatch();
    function closeDialog() {
        dispatch(closeNewTopicDialog());
    }

    function mapCategory(type: TopicsCardType) {
        switch (type) {
            case TopicsCardType.Good:
                return "Good";
            case TopicsCardType.Bad:
                return "Not good";
            case TopicsCardType.Improvements:
                return "To improve";
            default:
                return "";
        }
    }

    function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const formJson = Object.fromEntries((formData as any).entries());
        dispatch(addTopic({title: formJson.topicTitle, category: category!}));
        closeDialog();
    }

    return (
        <Dialog
            open={open}
            PaperProps={{
                component: 'form',
                onSubmit: handleSubmit,
            }}
        >
            <DialogTitle>New topic</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    Enter title of your retrospective topic for category:
                    <Typography variant="h4" component="span" sx={{display: 'block'}}>
                    {mapCategory(category || TopicsCardType.Good)}
                    </Typography>
                </DialogContentText>
                <TextField
                    autoFocus
                    required
                    margin="dense"
                    id="topicTitle"
                    name="topicTitle"
                    label="New topic name"
                    type="text"
                    fullWidth
                    variant="standard"
                />
            </DialogContent>
            <DialogActions>
                <Button type="button" onClick={closeDialog}>Cancel</Button>
                <Button type="submit" variant="contained" color="primary" startIcon={<AddIcon />}>Add</Button>
            </DialogActions>
        </Dialog>
    )
}
