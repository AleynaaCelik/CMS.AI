import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from 'react-query';
import MainLayout from './layouts/MainLayout';
import Dashboard from './pages/Dashboard';
import ContentList from './pages/content/ContentList';
import ContentDetail from './pages/content/ContentDetail';
import ContentCreate from './pages/content/ContentCreate';
import ContentEdit from './pages/content/ContentEdit';
import SearchPage from './pages/SearchPage';
import SettingsPage from './pages/SettingsPage';
import NotFoundPage from './pages/NotFoundPage';

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<MainLayout />}>
            <Route index element={<Dashboard />} />
            <Route path="content" element={<ContentList />} />
            <Route path="content/create" element={<ContentCreate />} />
            <Route path="content/:id" element={<ContentDetail />} />
            <Route path="content/:id/edit" element={<ContentEdit />} />
            <Route path="search" element={<SearchPage />} />
            <Route path="settings" element={<SettingsPage />} />
            <Route path="*" element={<NotFoundPage />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;