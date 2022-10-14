import Button from '@mui/material/Button';
import NavLinkAdapter from '@fuse/core/NavLinkAdapter';
import { useParams } from 'react-router-dom';
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import FuseLoading from '@fuse/core/FuseLoading';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';
import Chip from '@mui/material/Chip';
import Divider from '@mui/material/Divider';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import Box from '@mui/system/Box';
import format from 'date-fns/format';
import _ from '@lodash';
import { getContact, selectContact } from '../store/contactSlice';
import { selectRoles } from '../store/rolesSlice';

const ContactView = () => {
  const contact = useSelector(selectContact);
  const roles = useSelector(selectRoles);
  const routeParams = useParams();
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(getContact(routeParams.id));
  }, [dispatch, routeParams]);

  if (!contact || !roles) {
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
              src={
                contact.avatar && `assets/images/avatars/${contact.schoolCode}/${contact.avatar}`
              }
              alt={contact.firstName}
            >
              {contact.lastName.charAt(0)}
            </Avatar>
            <div className="flex items-center ml-auto mb-4">
              <Button variant="contained" color="secondary" component={NavLinkAdapter} to="edit">
                <FuseSvgIcon size={20}>heroicons-outline:pencil-alt</FuseSvgIcon>
                <span className="mx-8">Edit</span>
              </Button>
            </div>
          </div>

          <Typography className="mt-12 text-3xl font-bold truncate">
            {`${contact.firstName} ${contact.middleName} ${contact.lastName}`}
          </Typography>

          <div className="flex flex-wrap items-center mt-8">
            {contact.roles.map((id) => (
              <Chip
                key={id}
                label={_.find(roles, (role) => role.id === id).name}
                className="mr-12 mb-12"
                size="small"
              />
            ))}
          </div>

          <Divider className="mt-16 mb-24" />

          <div className="flex flex-col space-y-32">
            {contact.title && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:briefcase</FuseSvgIcon>
                <div className="ml-24 leading-6">{contact.title}</div>
              </div>
            )}
            {contact.staffTypeName && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:identification</FuseSvgIcon>
                <div className="ml-24 leading-6">{contact.staffTypeName}</div>
              </div>
            )}
            {contact.schoolName && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:office-building</FuseSvgIcon>
                <div className="ml-24 leading-6">{contact.schoolName}</div>
              </div>
            )}

            <div className="flex">
              <FuseSvgIcon>heroicons-outline:at-symbol</FuseSvgIcon>
              <div className="min-w-0 ml-24 space-y-4">
                <div className="flex items-center leading-6" key={contact.email}>
                  <a
                    className="hover:underline text-primary-500"
                    href={`mailto: ${contact.email}`}
                    target="_blank"
                    rel="noreferrer"
                  >
                    {contact.email}
                  </a>
                </div>
              </div>
            </div>

            <div className="flex">
              <FuseSvgIcon>heroicons-outline:phone</FuseSvgIcon>
              <div className="min-w-0 ml-24 space-y-4">
                <div className="flex items-center leading-6">
                  <div className="ml-10 font-mono">{contact.phoneNumber}</div>
                </div>
              </div>
            </div>

            {contact.birthdate && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:cake</FuseSvgIcon>
                <div className="ml-24 leading-6">
                  {format(new Date(contact.birthdate), 'MMMM d, y')}
                </div>
              </div>
            )}
            {contact.birthdate && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:clock</FuseSvgIcon>
                <div className="ml-24 leading-6">
                  {format(new Date(contact.dateJoined), 'MMMM d, y')}
                </div>
              </div>
            )}
            {contact.postalAddress && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:mail</FuseSvgIcon>
                <div className="ml-24 leading-6">{contact.postalAddress}</div>
              </div>
            )}
            {contact.streetAddress && (
              <div className="flex items-center">
                <FuseSvgIcon>heroicons-outline:location-marker</FuseSvgIcon>
                <div className="ml-24 leading-6">{contact.streetAddress}</div>
              </div>
            )}

            {contact.notes && (
              <div className="flex">
                <FuseSvgIcon>heroicons-outline:menu-alt-2</FuseSvgIcon>
                <div
                  className="max-w-none ml-24 prose dark:prose-invert"
                  dangerouslySetInnerHTML={{ __html: contact.notes }}
                />
              </div>
            )}
          </div>
        </div>
      </div>
    </>
  );
};

export default ContactView;
