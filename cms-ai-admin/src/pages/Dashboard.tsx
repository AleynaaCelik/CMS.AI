import React from 'react';
import { 
  Typography, 
  Grid, 
  Paper, 
  Box,
  Card,
  CardContent,
  CardActionArea
} from '@mui/material';
import { Link } from 'react-router-dom';
import ArticleIcon from '@mui/icons-material/Article';
import AutoFixHighIcon from '@mui/icons-material/AutoFixHigh';
import SearchIcon from '@mui/icons-material/Search';

const Dashboard: React.FC = () => {
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
        <Typography variant="body1">
          No recent content to display.
        </Typography>
      </Paper>
    </>
  );
};

export default Dashboard;