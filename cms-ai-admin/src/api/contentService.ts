import apiClient from './apiClient';
import { Content, ContentCreateDto, ContentUpdateDto } from '../types/content';

export const contentService = {
  getAll: async (): Promise<Content[]> => {
    const response = await apiClient.get('/content');
    return response.data;
  },
  
  getById: async (id: string): Promise<Content> => {
    const response = await apiClient.get(`/content/${id}`);
    return response.data;
  },
  
  create: async (data: ContentCreateDto): Promise<Content> => {
    const response = await apiClient.post('/content', data);
    return response.data;
  },
  
  update: async (id: string, data: ContentUpdateDto): Promise<void> => {
    await apiClient.put(`/content/${id}`, data);
  },
  
  delete: async (id: string): Promise<void> => {
    await apiClient.delete(`/content/${id}`);
  },
  
  analyzeWithAI: async (id: string): Promise<any> => {
    const response = await apiClient.post(`/ai/analyze/${id}`);
    return response.data;
  },
  
  enhanceWithAI: async (id: string): Promise<Content> => {
    const response = await apiClient.post(`/ai/enhance/${id}`);
    return response.data;
  }
};