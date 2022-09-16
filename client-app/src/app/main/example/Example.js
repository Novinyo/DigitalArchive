import { styled } from '@mui/material/styles';
import { useTranslation } from 'react-i18next';
import FusePageSimple from '@fuse/core/FusePageSimple';
import DemoContent from '@fuse/core/DemoContent';

const Root = styled(FusePageSimple)(({ theme }) => ({
  '& .FusePageSimple-header': {
    backgroundColor: theme.palette.background.paper,
    borderBottomWidth: 2,
    borderStyle: 'solid',
    borderColor: theme.palette.divider,
  },
  '& .FusePageSimple-toolbar': {},
  '& .FusePageSimple-content': {},
  '& .FusePageSimple-sidebarHeader': {},
  '& .FusePageSimple-sidebarContent': {},
}));

function ExamplePage(props) {
  const { t } = useTranslation('examplePage');

  return (
    <Root
      header={
        <div className="p-24">
          <h4>Example Page</h4>
        </div>
      }
      content={
        <div className="p-24">
          <h3>Content</h3>
          <br />
          <DemoContent />
        </div>
      }
      scroll="content"
    />
  );
}

export default ExamplePage;
