import { darken, lighten } from '@mui/material/styles';
import Typography from '@mui/material/Typography';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Chip from '@mui/material/Chip';
import clsx from 'clsx';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';

function StudentCard(props) {
  const { title, parent } = props;
  return (
    <Card className="flex flex-col h-200 shadow">
      <CardContent className="flex flex-col flex-auto p-24">
        <div className={clsx('w-full space-y-16', '')}>
          <div className="flex items-center justify-between mb-16">
            <Chip
              className="font-semibold text-12"
              label={`${title}'s info`}
              sx={{
                color: (theme) =>
                  theme.palette.mode === 'light' ? darken('#2196f3', 0.4) : lighten('#2196f3', 0.8),
                backgroundColor: (theme) =>
                  theme.palette.mode === 'light' ? lighten('#2196f3', 0.8) : darken('#2196f3', 0.1),
              }}
              size="small"
            />
          </div>
          <Typography className="text-16 font-medium">
            {parent.firstName}, {parent.lastName}
          </Typography>
          <div className="flex">
            <FuseSvgIcon>heroicons-outline:at-symbol</FuseSvgIcon>
            <div className="min-w-0 ml-24 space-y-4">
              <div className="flex items-center leading-6" key={parent.email}>
                <a
                  className="hover:underline text-primary-500"
                  href={`mailto: ${parent.email}`}
                  target="_blank"
                  rel="noreferrer"
                >
                  {parent.email}
                </a>
              </div>
            </div>
          </div>

          <div className="flex">
            <FuseSvgIcon>heroicons-outline:phone</FuseSvgIcon>
            <div className="min-w-0 ml-24 space-y-4">
              <div className="flex items-center leading-6">
                <div className="ml-10 font-mono">{parent.phone}</div>
              </div>
            </div>
          </div>
        </div>
      </CardContent>
    </Card>
  );
}

export default StudentCard;
