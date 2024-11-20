export async function fetchEmployees() {
  const response = await fetch('/api/employees'); // Запрос на Backend
  if (!response.ok) {
    throw new Error('Failed to fetch employees');
  }
  return response.json(); // Возвращаем данные
}
