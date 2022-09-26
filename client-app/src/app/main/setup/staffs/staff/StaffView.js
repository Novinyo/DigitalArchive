import Button from '@mui/material/Button';
import NavLinkAdapter from '@fuse/core/NavLinkAdapter';
import { useParams } from 'react-router-dom';
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import FuseLoading from '@fuse/core/FuseLoading';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import Box from '@mui/system/Box';
import format from 'date-fns/format';
import { getStaff, selectStaff } from '../store/staffSlice';

const StaffView = () => {
  const staff = useSelector(selectStaff);
  const routeParams = useParams();
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(getStaff(routeParams.id));
  }, [dispatch, routeParams]);

  if (!staff) {
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
        {staff.background && (
          <img
            className="absolute inset-0 object-cover w-full h-full"
            src={staff.background}
            alt="user background"
          />
        )}
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
              src={staff.avatar}
              alt={staff.name}
            >
              {staff.name.charAt(0)}
            </Avatar>
            <div className="flex items-center ml-auto mb-4">
              <Button variant="contained" color="secondary" component={NavLinkAdapter} to="edit">
                <FuseSvgIcon size={20}>heroicons-outline:pencil-alt</FuseSvgIcon>
                <span className="mx-8">Edit</span>
              </Button>
            </div>
          </div>

          <Typography className="mt-12 text-4xl font-bold truncate">{staff.name}</Typography>

          <Divider className="mt-16 mb-24" />

          <div className="flex flex-col space-y-32">
            {staff.title && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:briefcase</FuseSvgIcon>
                <div className="ml-24 leading-6">{staff.title}</div>
              </div>
            )}

            {staff.company && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:office-building</FuseSvgIcon>
                <div className="ml-24 leading-6">{staff.company}</div>
              </div>
            )}

            {staff.emails.length && staff.emails.some((item) => item.email.length > 0) && (
              <div className="flex">
                <FuseSvgIcon>heroicons-outline:mail</FuseSvgIcon>
                <div className="min-w-0 ml-24 space-y-4">
                  {staff.emails.map(
                    (item) =>
                      item.email !== '' && (
                        <div className="flex items-center leading-6" key={item.email}>
                          <a
                            className="hover:underline text-primary-500"
                            href={`mailto: ${item.email}`}
                            target="_blank"
                            rel="noreferrer"
                          >
                            {item.email}
                          </a>
                        </div>
                      )
                  )}
                </div>
              </div>
            )}

            {staff.phoneNumbers.length &&
              staff.phoneNumbers.some((item) => item.phoneNumber.length > 0) && (
                <div className="flex">
                  <FuseSvgIcon>heroicons-outline:phone</FuseSvgIcon>
                  <div className="min-w-0 ml-24 space-y-4">
                    {staff.phoneNumbers.map(
                      (item, index) =>
                        item.phoneNumber !== '' && (
                          <div className="flex items-center leading-6" key={index}>
                            <Box
                              className="hidden sm:flex w-24 h-16 overflow-hidden"
                              sx={{
                                background:
                                  "url('/assets/images/apps/contacts/flags.png') no-repeat 0 0",
                                backgroundSize: '24px 3876px',
                                // backgroundPosition: getCountryByIso(item.country)?.flagImagePos,
                              }}
                            />

                            <div className="ml-10 font-mono">{item.phoneNumber}</div>
                          </div>
                        )
                    )}
                  </div>
                </div>
              )}

            {staff.address && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:location-marker</FuseSvgIcon>
                <div className="ml-24 leading-6">{staff.address}</div>
              </div>
            )}

            {staff.birthday && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:cake</FuseSvgIcon>
                <div className="ml-24 leading-6">
                  {format(new Date(staff.birthday), 'MMMM d, y')}
                </div>
              </div>
            )}

            {staff.notes && (
              <div className="flex">
                <FuseSvgIcon>heroicons-outline:menu-alt-2</FuseSvgIcon>
                <div
                  className="max-w-none ml-24 prose dark:prose-invert"
                  dangerouslySetInnerHTML={{ __html: staff.notes }}
                />
              </div>
            )}
          </div>
        </div>
      </div>
    </>
  );
};

export default StaffView;
