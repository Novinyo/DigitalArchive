import PropTypes from 'prop-types';
import Button from '@mui/material/Button';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import TextField from '@mui/material/TextField';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import Divider from '@mui/material/Divider';
import { styled } from '@mui/material/styles';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import IconButton from '@mui/material/IconButton';
import { useDeepCompareEffect } from '@fuse/hooks';
import CloseIcon from '@mui/icons-material/Close';
import { useDispatch, useSelector } from 'react-redux';
import { useState } from 'react';
import { selectDocumentTypes, getDocumentTypes } from '../../store/documentTypesSlice';

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
  '& .MuiDialogContent-root': {
    padding: theme.spacing(2),
  },
  '& .MuiDialogActions-root': {
    padding: theme.spacing(1),
  },
}));

function BootstrapDialogTitle(props) {
  const { children, onClose, ...other } = props;

  return (
    <DialogTitle sx={{ m: 0, p: 2 }} {...other}>
      {children}
      {onClose ? (
        <IconButton
          aria-label="close"
          onClick={onClose}
          sx={{
            position: 'absolute',
            right: 8,
            top: 8,
            color: (theme) => theme.palette.grey[500],
          }}
        >
          <CloseIcon />
        </IconButton>
      ) : null}
    </DialogTitle>
  );
}

BootstrapDialogTitle.propTypes = {
  children: PropTypes.node,
  onClose: PropTypes.func.isRequired,
};

function DocumentsTab() {
  const [docVal, setDocVal] = useState({
    docType: '',
    docName: '',
  });
  const [canSave, setCanSave] = useState(true);
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const dispatch = useDispatch();
  const documentTypes = useSelector(selectDocumentTypes);

  useDeepCompareEffect(() => {
    dispatch(getDocumentTypes());
  }, [dispatch]);
  const handleChange = (evt) => {
    setDocVal({
      ...docVal,
      [evt.target.name]: evt.target.value,
    });
  };

  const handleSubmit = () => {
    handleClose();
  };
  return (
    <div>
      <Button
        className="mx-8 whitespace-nowrap"
        variant="contained"
        color="secondary"
        onClick={handleOpen}
        startIcon={<FuseSvgIcon size={20}>heroicons-outline:plus</FuseSvgIcon>}
      >
        Add document
      </Button>
      <BootstrapDialog
        open={open}
        onClose={handleClose}
        aria-labelledby="customized-dialog-title"
        disableEscapeKeyDown={open}
      >
        <BootstrapDialogTitle id="customized-dialog-title" onClose={handleClose}>
          New Document
        </BootstrapDialogTitle>
        <DialogContent dividers>
          <form onSubmit={handleSubmit}>
            <Divider className="mt-16 mb-24" />
            <FormControl fullWidth>
              <InputLabel id="lblDocType">Document Type</InputLabel>
              <Select
                labelId="lblDocType"
                id="docType"
                value={docVal.docType}
                name="docType"
                label="Document Type"
                onChange={handleChange}
              >
                {documentTypes.map((item) => (
                  <MenuItem key={item.id} value={item.id}>
                    {item.name}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
            <TextField
              className="mt-32"
              label="Document Name"
              placeholder="Type document name"
              id="docName"
              name="docName"
              variant="outlined"
              value={docVal.docName}
              onChange={handleChange}
              required
              fullWidth
            />
          </form>
        </DialogContent>
        <DialogActions>
          <Button autoFocus disabled={canSave} onClick={handleSubmit}>
            Save changes
          </Button>
        </DialogActions>
      </BootstrapDialog>
    </div>
  );
}

export default DocumentsTab;
