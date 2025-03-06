import React from 'react';
import { useQuery } from 'react-query';
import { 
  Typography, 
  Grid, 
  Paper, 
  Box, 
  Card, 
  CardContent, 
  CardActionArea,
  Divider,
  List,
  ListItem,
  ListItemText,
  CircularProgress
} from '@mui/material';
import ArticleIcon from '@mui/icons-material/Article';
import AutoFixHighIcon from '@mui/icons-material/AutoFixHigh';
import SearchIcon from '@mui/icons-material/Search';
import { Link } from 'react-router-dom';
import { contentService } from '../api/contentService';

const Dashboard: React.FC = () => {
  const { data: contents, isLoading } = useQuery('dashboardContents', () => 
    contentService.getAll().then(data => data.slice(0, 5))
  );

  return (
    <>
      <Typography variant="h4" gutterBottom>Dashboard</Typography>
      
      <Grid container spacing={3} sx={{ mb: 4 }}>
        <Grid item xs={12} md={4}>
          <Card sx={{ height: '100%' }}>
            <CardActionArea component={Link} to="/content">
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                  <ArticleIcon fontSize="large" color="primary" sx={{ mr: 1 }} />
                  <Typography variant="h5">Content Management</Typography>
                </Box>
                <Typography variant="body2" color="text.secondary">
                  Create, edit, and manage your content items
                </Typography>
              </CardContent>
            </CardActionArea>
          </Card>
        </Grid>
        
        <Grid item xs={12} md={4}>
          <Card sx={{ height: '100%' }}>
            <CardActionArea component={Link} to="/content/create">
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                  <AutoFixHighIcon fontSize="large" color="secondary" sx={{ mr: 1 }} />
                  <Typography variant="h5">AI Content Creation</Typography>
                </Box>
                <Typography variant="body2" color="text.secondary">
                  Leverage AI to create, enhance, and optimize content
                </Typography>
              </CardContent>
            </CardActionArea>
          </Card>
        </Grid>
        
        <Grid item xs={12} md={4}>
          <Card sx={{ height: '100%' }}>
            <CardActionArea component={Link} to="/search">
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                  <SearchIcon fontSize="large" color="info" sx={{ mr: 1 }} />
                  <Typography variant="h5">Search & Discovery</Typography>
                </Box>
                <Typography variant="body2" color="text.secondary">
                  Find and discover content with advanced search features
                </Typography>
              </CardContent>
            </CardActionArea>
          </Card>
        </Grid>
      </Grid>
      
      <Paper sx={{ p: 2 }}>
        <Typography variant="h6" gutterBottom>Recent Content</Typography>
        <Divider sx={{ mb: 2 }} />
        
        {isLoading ? (
          <Box sx={{ display: 'flex', justifyContent: 'center', py: 2 }}>
            <CircularProgress />
          </Box>
        ) : (
          <List>
            {contents?.map((content) => (
              <ListItem 
                key={content.id} 
                button 
                component={Link} 
                to={`/content/${content.id}`}
                divider
              >
                <ListItemText 
                  primary={content.title} 
                  secondary={`Last modified: ${new Date(
                    content.lastModifiedAt || content.createdAt
                  ).toLocaleDateString()}`} 
                />
              </ListItem>
            ))}
            {(!contents || contents.length === 0) && (
              <ListItem>
                <ListItemText primary="No content found" />
              </ListItem>
            )}
          </List>
        )}
      </Paper>
    </>
  );
};

export default Dashboard;