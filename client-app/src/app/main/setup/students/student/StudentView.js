import Button from '@mui/material/Button';
import NavLinkAdapter from '@fuse/core/NavLinkAdapter';
import { useParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import FuseLoading from '@fuse/core/FuseLoading';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';
import Chip from '@mui/material/Chip';
import Divider from '@mui/material/Divider';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import Box from '@mui/system/Box';
import format from 'date-fns/format';
import { useEffect, useState } from 'react';
import { getStudent, selectStudent } from '../store/studentSlice';
import StudentCard from './StudentCard';

function StudentView(props) {
  const student = useSelector(selectStudent);
  const routeParams = useParams();
  const dispatch = useDispatch();
  const [father, setFather] = useState({});
  const [mother, setMother] = useState({});

  useEffect(() => {
    if (student) {
      setFather({
        firstName: student.faFName,
        lastName: student.faLName,
        email: student.faEmail,
        phone: student.faPhone,
      });

      setMother({
        firstName: student.moFName,
        lastName: student.moLName,
        email: student.moEmail,
        phone: student.moPhone,
      });
    }
  }, [student]);

  useEffect(() => {
    dispatch(getStudent(routeParams.id));
  }, [dispatch, routeParams]);

  if (!student) {
    return <FuseLoading />;
  }
  return (
    <>
      <Box
        className="relative w-full h-160 sm:h-192 px-32 sm:px-48"
        sx={{
          backgroundColor: 'background.default',
        }}
      >
        <img
          className="absolute inset-0 object-cover w-full h-full"
          src="assets/images/cards/14-640x480.jpg"
          alt="user background"
        />
      </Box>
      <div className="relative flex flex-col flex-auto items-center p-24 pt-0 sm:p-48 sm:pt-0">
        <div className="w-full max-w-3xl">
          <div className="flex flex-auto items-end -mt-64">
            <Avatar
              sx={{
                borderWidth: 4,
                borderStyle: 'solid',
                borderColor: 'background.paper',
                backgroundColor: 'background.default',
                color: 'text.secondary',
              }}
              className="w-128 h-128 text-64 font-bold"
              src=""
              alt=""
            >
              {student.lastName.charAt(0)}
            </Avatar>
            <div className="flex items-center ml-auto mb-4">
              <Button variant="contained" color="secondary" component={NavLinkAdapter} to="edit">
                <FuseSvgIcon size={20}>heroicons-outline:pencil-alt</FuseSvgIcon>
                <span className="mx-8">Edit</span>
              </Button>
            </div>
          </div>
          <Typography className="mt-12 text-4xl font-bold truncate">
            {`${student.firstName} ${student.middleName} ${student.lastName}`}
          </Typography>
          <Chip
            label={student.gender === 0 ? 'Male' : 'Female'}
            className="mr-12 mb-12"
            size="small"
          />
          <Divider className="mt-16 mb-24" />
          <div className="flex flex-col space-y-24">
            {student.birthdate && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:cake</FuseSvgIcon>
                <div className="ml-24 leading-6">
                  {format(new Date(student.birthdate), 'MMMM d, y')}
                </div>
              </div>
            )}
            {student.dateJoined && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:clock</FuseSvgIcon>
                <div className="ml-24 leading-6">
                  {format(new Date(student.dateJoined), 'MMMM d, y')}
                </div>
              </div>
            )}
            <div className="flex items-center">
              <FuseSvgIcon className="text-48" size={24} color="action">
                material-solid:priority_high
              </FuseSvgIcon>
              <div className="ml-24 leading-6">{student.emergencyContact}</div>
            </div>
            <div className="flex grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-2 gap-16 mt-32 sm:mt-40">
              <StudentCard parent={father} title="Father" />
              <StudentCard parent={mother} title="Mother" />
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default StudentView;
