import { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import CloseIcon from '@mui/icons-material/Close';
import TextField from '@mui/material/TextField';
import InputLabel from '@mui/material/InputLabel';
import Button from '@mui/material/Button';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import Grid from '@mui/material/Grid';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import Divider from '@mui/material/Divider';
import { styled } from '@mui/material/styles';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { useDispatch, useSelector } from 'react-redux';
import { useDeepCompareEffect } from '@fuse/hooks';
import Box from '@mui/system/Box';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import IconButton from '@mui/material/IconButton';
import { Viewer, Worker } from '@react-pdf-viewer/core';
import { selectDocumentTypes, getDocumentTypes } from '../../store/documentTypesSlice';
import '@react-pdf-viewer/core/lib/styles/index.css';

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

function DocumentModal(props) {
  const { open, setOpen, handleSubmit, setDocVal, docVal, url, setUrl } = props;

  const [canSave, setCanSave] = useState(true);
  const handleClose = () => setOpen(false);
  const dispatch = useDispatch();
  const documentTypes = useSelector(selectDocumentTypes);

  useEffect(() => {
    if (
      docVal.docName.trim().length === 0 ||
      docVal.docTypeId.trim().length === 0 ||
      docVal.docUrl.trim().length === 0
    ) {
      setCanSave(true);
    } else {
      setCanSave(false);
    }
  }, [docVal]);

  useDeepCompareEffect(() => {
    dispatch(getDocumentTypes());
  }, [dispatch]);

  const handleChange = (evt) => {
    console.log(evt);
    setDocVal({
      ...docVal,
      [evt.target.name]: evt.target.value,
    });
  };

  return (
    <BootstrapDialog
      open={open}
      fullWidth
      maxWidth="md"
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
              id="docTypeId"
              value={docVal.docTypeId}
              name="docTypeId"
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
            autoComplete="false"
            variant="outlined"
            value={docVal.docName}
            onChange={handleChange}
            required
            fullWidth
          />
          <Grid container spacing={2}>
            <Grid item xs={12} sm={6}>
              <DatePicker
                className="w-full"
                clearable
                onChange={(evt) => {
                  setDocVal({ ...docVal, startDate: evt });
                }}
                value={docVal.startDate}
                renderInput={(params) => (
                  <TextField
                    {...params}
                    className="mt-32"
                    label="Start Date"
                    type="date"
                    id="startDate"
                    InputLabelProps={{
                      shrink: true,
                    }}
                    variant="outlined"
                    fullWidth
                  />
                )}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <DatePicker
                className="w-full"
                clearable
                onChange={(evt) => {
                  setDocVal({ ...docVal, endDate: evt });
                }}
                value={docVal.endDate}
                renderInput={(params) => (
                  <TextField
                    {...params}
                    className="mt-32"
                    id="endDate"
                    label="Start Date"
                    type="date"
                    InputLabelProps={{
                      shrink: true,
                    }}
                    variant="outlined"
                    fullWidth
                  />
                )}
              />
            </Grid>
          </Grid>
          <Box
            sx={{
              borderWidth: 2,
              borderStyle: 'solid',
              borderColor: 'background.paper',
            }}
            className="relative flex items-center justify-center w-60 h-60 my-8 shadow rounded overflow-hidden"
          >
            <div className="absolute inset-0 bg-black bg-opacity-50 z-10" />
            <div className="absolute inset-0 flex items-center justify-center z-20">
              <div>
                <label htmlFor="button-avatar" className="flex p-8 cursor-pointer">
                  <input
                    accept="application/pdf"
                    className="hidden"
                    id="button-avatar"
                    type="file"
                    onChange={async (e) => {
                      function readFileAsync() {
                        return new Promise((resolve, reject) => {
                          const file = e.target.files[0];

                          if (!file) {
                            return;
                          }
                          if (file.type !== 'application/pdf') {
                            alert('Only pdf files are allowed');
                            return;
                          }
                          if (file.size > 15000000) {
                            alert('File size cannot exceed 15MB');
                            return;
                          }
                          setUrl(URL.createObjectURL(file));
                          const reader = new FileReader();

                          reader.onload = () => {
                            resolve(
                              setDocVal({
                                ...docVal,
                                docUrl: `data:${file.type};base64,${window.btoa(reader.result)}`,
                              })
                            );
                          };

                          reader.onerror = reject;

                          reader.readAsBinaryString(file);
                        });
                      }

                      await readFileAsync();
                    }}
                  />
                  <FuseSvgIcon size={32} className="text-white">
                    heroicons-outline:upload
                  </FuseSvgIcon>
                </label>
              </div>
            </div>
          </Box>
          <div sx={{ width: 800, height: 450 }}>
            {url ? (
              <div style={{ height: '450px' }}>
                <Worker workerUrl="https://unpkg.com/pdfjs-dist@2.15.349/build/pdf.worker.min.js">
                  <Viewer fileUrl={url} />
                </Worker>
              </div>
            ) : (
              <div className="relative flex items-center justify-center overflow-hidden">
                Preview area
              </div>
            )}
          </div>
        </form>
      </DialogContent>
      <DialogActions>
        <Button variant="contained" color="primary" onClick={handleClose}>
          Cancel
        </Button>
        <Button
          autoFocus
          disabled={canSave}
          onClick={handleSubmit}
          variant="contained"
          color="success"
        >
          Save changes
        </Button>
      </DialogActions>
    </BootstrapDialog>
  );
}

export default DocumentModal;
