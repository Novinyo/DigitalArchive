import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import CardContent from '@mui/material/CardContent';
import CardActions from '@mui/material/CardActions';
import Avatar from '@mui/material/Avatar';
import IconButton from '@mui/material/IconButton';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import { useState } from 'react';
import { Viewer, Worker } from '@react-pdf-viewer/core';
import { red } from '@mui/material/colors';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { Controller, useFormContext } from 'react-hook-form';
import FuseUtils from '@fuse/utils';
import DocumentModal from './DocumentModal';

export const pageThumbnailPlugin = (props) => {
  const { PageThumbnail } = props;

  return {
    renderViewer: (renderProps) => {
      const { slot } = renderProps;
      slot.children = PageThumbnail;
      // reset the sub slot
      slot.subSlot.attrs = {};
      slot.subSlot.children = <></>;
      return slot;
    },
  };
};
function DocumentsTab() {
  const methods = useFormContext();
  const { control, formState, setValue, getValues } = methods;
  const { isValid, dirtyFields, errors } = formState;

  const [docVal, setDocVal] = useState({
    id: 0,
    docTypeId: '',
    docName: '',
    docUrl: '',
    startDate: null,
    endDate: null,
  });

  const [open, setOpen] = useState(false);
  const [url, setUrl] = useState('');
  const [edit, setEdit] = useState(false);

  const handleOpen = () => {
    setDocVal({
      id: FuseUtils.generateGUID(),
      docUrl: '',
      docName: '',
      docTypeId: '',
      startDate: null,
      endDate: null,
    });

    setUrl('');
    setOpen(true);
    setEdit(false);
  };
  const handleClose = () => setOpen(false);

  const handleSubmit = () => {
    const documents = getValues('documents');
    if (edit) {
      const document = documents.find((x) => x.id === docVal.id);
      if (document) {
        document.docName = docVal.docName;
        document.docUrl = docVal.docUrl;
        document.docTypeId = docVal.docTypeId;
      }
    } else {
      setValue('documents', [...documents, docVal]);
    }

    handleClose();
  };

  const removeDoc = (id) => {
    const documents = getValues('documents');
    const newDocs = documents.filter((x) => x.id !== id);

    console.log(newDocs);

    setValue('documents', newDocs);
  };

  const changeDoc = (id) => {
    const documents = getValues('documents');
    const document = documents.find((x) => x.id === id);

    setDocVal({
      id: document.id,
      docUrl: document.docUrl,
      docName: document.docName,
      docTypeId: document.docTypeId,
      startDate: document.startDate,
      endDate: document.endDate,
    });
    const blob = base64ToBlob(document.docUrl);
    const currentUrl = URL.createObjectURL(blob);
    setUrl(currentUrl);
    setOpen(true);
    setEdit(true);
  };

  const base64ToBlob = (dataurl) => {
    const baseNoPrefix = dataurl.substr('data:application/pdf;base64,'.length);

    const bytes = atob(baseNoPrefix);
    let n = bytes.length;
    const u8arr = new Uint8Array(n);

    // eslint-disable-next-line no-plusplus
    while (n--) {
      u8arr[n] = bytes.charCodeAt(n);
    }

    return new Blob([u8arr], { type: 'application/pdf' });
  };

  return (
    <div>
      <Button
        className="mx-4 whitespace-nowrap"
        variant="contained"
        color="secondary"
        onClick={handleOpen}
        startIcon={<FuseSvgIcon size={20}>heroicons-outline:plus</FuseSvgIcon>}
      >
        Add document
      </Button>
      <Controller
        name="documents"
        control={control}
        render={({ field }) => (
          <Grid container spacing={1} id="documents" className="mt-32 sm:mt-40">
            {field.value.map((m, i) => {
              const blob = base64ToBlob(m.docUrl);
              const currentUrl = URL.createObjectURL(blob);

              return (
                <Grid key={m.id} item xs={12} sm={6} {...field}>
                  <Card sx={{ maxWidth: 200 }}>
                    <CardHeader
                      avatar={
                        <Avatar sx={{ bgcolor: red[500] }} aria-label="document">
                          D
                        </Avatar>
                      }
                      title={m.docName}
                    />
                    <CardContent className="h-128">
                      <Worker workerUrl="https://unpkg.com/pdfjs-dist@2.15.349/build/pdf.worker.min.js">
                        <Viewer fileUrl={currentUrl} className="flex items-center justify-center" />
                      </Worker>
                    </CardContent>
                    <CardActions className="flex items-center justify-center">
                      <IconButton
                        onClick={() => {
                          changeDoc(m.id);
                        }}
                      >
                        <EditIcon />
                      </IconButton>
                      <IconButton
                        onClick={() => {
                          removeDoc(m.id);
                        }}
                      >
                        <DeleteIcon />
                      </IconButton>
                    </CardActions>
                  </Card>
                </Grid>
              );
            })}
          </Grid>
        )}
      />
      <DocumentModal
        open={open}
        setOpen={setOpen}
        handleSubmit={handleSubmit}
        docVal={docVal}
        setDocVal={setDocVal}
        url={url}
        setUrl={setUrl}
      />
    </div>
  );
}

export default DocumentsTab;
