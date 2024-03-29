import { styled } from '@mui/material/styles';
import FusePageSimple from '@fuse/core/FusePageSimple';

const Root = styled(FusePageSimple)(({ theme }) => ({
  '& .FusePageSimple-header': {
    backgroundColor: theme.palette.background.paper,
    borderBottomWidth: 0,
    borderStyle: 'solid',
    borderColor: theme.palette.divider,
  },
  '& .FusePageSimple-toolbar': {},
  '& .FusePageSimple-content': {},
  '& .FusePageSimple-sidebarHeader': {},
  '& .FusePageSimple-sidebarContent': {},
}));
function HomePage() {
  return (
    <Root
      header={
        <div className="p-24">
          <h4>Home Page</h4>
        </div>
      }
      content={
        <div className="p-24">
          <h3>Content</h3>
          <br />
          <div>
            <h1>All about home page</h1>
          </div>
        </div>
      }
      scroll="content"
    />
  );
}

export default HomePage;
