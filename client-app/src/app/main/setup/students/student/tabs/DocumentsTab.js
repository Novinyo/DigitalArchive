import Button from "@mui/material/Button";
import FuseSvgIcon from "@fuse/core/FuseSvgIcon";
import { useState } from "react";

import { Viewer } from "@react-pdf-viewer/core";
import { Controller, useFormContext } from "react-hook-form";
import DocumentModal from "./DocumentModal";



function DocumentsTab() {
  const methods = useFormContext();
  const { control, formState } = methods;
  const [docVal, setDocVal] = useState({
    docType: "",
    docName: "",
    url:"",
    file: null
  });

  const [open, setOpen] = useState(false);
  const [url, setUrl] = useState('');
  const [documents, setDocuments] = useState([]);

  const handleOpen = () => {
    setDocVal({
      url:'',
      docName:'',
      docType: '',
      file: null
    });

    setUrl('');
    setOpen(true)
  };
  const handleClose = () => setOpen(false);

  const handleSubmit = () => {
    setDocuments([
      ...documents,
      docVal
    ])
    handleClose();
  };

  const base64ToBlob = (dataurl) => {
 
    var baseNoPrefix = dataurl.substr('data:application/pdf;base64,'.length);
      
    const bytes = atob(baseNoPrefix);
    let n = bytes.length;
    let u8arr = new Uint8Array(n);
        
    while(n--){
        u8arr[n] = bytes.charCodeAt(n);
    }
    
    return new Blob([u8arr], {type:'application/pdf'});
}
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
      <Controller 
      name="documents" 
      control={control} 
      render={({ field: {onChange, value }}) => 
          documents.map((m, i) => {
            
            const blob = base64ToBlob(m.url)
            const currentUrl = URL.createObjectURL(blob);
            
            return (
            <div key={i} className="flex items-center justify-center relative w-96 h-96 overflow-hidden my-8" md={4} xs={6}>
                <Viewer className="max-w-none w-auto h-full" fileUrl={currentUrl} />
            </div>
          )})
        } 
          />
      <DocumentModal open={open} setOpen={setOpen} handleSubmit={handleSubmit} docVal={docVal} setDocVal={setDocVal}/>
    </div>
  );
}

export default DocumentsTab;
