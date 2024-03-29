import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';
import ListItem from '@mui/material/ListItem';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import ListItemText from '@mui/material/ListItemText';
import Divider from '@mui/material/Divider';
import NavLinkAdapter from '@fuse/core/NavLinkAdapter';
import { format } from 'date-fns';

function StudentListItem(props) {
  const { student } = props;

  return (
    <>
      <ListItem
        className="px-32 py-16"
        sx={{ bgcolor: 'background.paper' }}
        button
        component={NavLinkAdapter}
        to={`/setup/students/${student.id}`}
      >
        <ListItemAvatar>
          <Avatar
            alt={student.firstName}
            src={`assets/images/avatars/${student.code}/${student?.avatar}`}
          />
        </ListItemAvatar>
        <ListItemText
          classes={{ root: 'm-0', primary: 'font-medium leading-5 truncate' }}
          primary={`${student.firstName} ${student.middleName} ${student.lastName}`}
          secondary={
            <>
              <Typography
                className="inline"
                component="span"
                variant="body2"
                color="text.secondary"
              >
                {student.gender === 0 ? 'Male' : 'Female'}
              </Typography>
              &nbsp;--&nbsp;
              <Typography
                className="inline"
                component="span"
                variant="body2"
                color="text.secondary"
              >
                {format(new Date(student.dateJoined), 'MM/dd/yyyy')}
              </Typography>
            </>
          }
        />
      </ListItem>
      <Divider />
    </>
  );
}

export default StudentListItem;
