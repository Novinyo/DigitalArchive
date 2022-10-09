import Button from '@mui/material/Button';
import { useFormContext } from 'react-hook-form';
import { useDispatch } from 'react-redux';
import NavLinkAdapter from '@fuse/core/NavLinkAdapter';
import Box from '@mui/system/Box';
import _ from '@lodash';
import { useNavigate, useParams } from 'react-router-dom';
import { addContact, removeContact, updateContact } from '../store/contactSlice';

function ContactFooter(props) {
  const dispatch = useDispatch();
  const methods = useFormContext();
  const { formState, watch, getValues } = methods;
  const { isValid, dirtyFields } = formState;
  const { id } = getValues();
  const routeParams = useParams();
  const navigate = useNavigate();

  console.log(formState);

  function onSubmit(data) {
    if (routeParams.id === 'new') {
      dispatch(addContact(data)).then(({ payload }) => {
        navigate(`/setup/contacts/${payload}`);
      });
    } else {
      dispatch(updateContact(data));
    }
  }

  function handleRemoveContact() {
    dispatch(removeContact(id)).then(() => {
      navigate('/setup/contacts');
    });
  }
  return (
    <Box
      className="flex items-center mt-40 py-14 pr-16 pl-4 sm:pr-48 sm:pl-36 border-t"
      sx={{ backgroundColor: 'background.default' }}
    >
      {routeParams.id !== 'new' && (
        <Button color="error" onClick={handleRemoveContact}>
          Delete
        </Button>
      )}
      <Button className="ml-auto" component={NavLinkAdapter} to={-1}>
        Cancel
      </Button>
      <Button
        className="ml-8"
        variant="contained"
        color="secondary"
        disabled={_.isEmpty(dirtyFields) || !isValid}
        onClick={onSubmit}
      >
        Save
      </Button>
    </Box>
  );
}

export default ContactFooter;
