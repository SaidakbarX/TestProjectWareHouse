import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import { 
  Package, 
  Ruler, 
  Users, 
  ArrowDownToLine, 
  ArrowUpFromLine, 
  BarChart3, 
  Warehouse 
} from 'lucide-react';

interface LayoutProps {
  children: React.ReactNode;
}

const navigation = [
  { name: 'Resources', href: '/resources', icon: Package },
  { name: 'Measurements', href: '/measurements', icon: Ruler },
  { name: 'Clients', href: '/clients', icon: Users },
  { name: 'Receptions', href: '/receptions', icon: ArrowDownToLine },
  { name: 'Shipments', href: '/shipments', icon: ArrowUpFromLine },
  { name: 'Balances', href: '/balances', icon: BarChart3 },
];

export const Layout: React.FC<LayoutProps> = ({ children }) => {
  const location = useLocation();

  return (
    <div className="min-h-screen bg-gray-50 flex">
      {/* Sidebar */}
      <div className="w-64 bg-white shadow-lg">
        <div className="p-6 border-b border-gray-200">
          <div className="flex items-center space-x-2">
            <Warehouse className="h-8 w-8 text-blue-600" />
            <h1 className="text-xl font-bold text-gray-900">Warehouse</h1>
          </div>
        </div>
        
        <nav className="mt-6">
          <div className="space-y-1 px-3">
            {navigation.map((item) => {
              const isActive = location.pathname === item.href;
              const Icon = item.icon;
              
              return (
                <Link
                  key={item.name}
                  to={item.href}
                  className={`group flex items-center px-3 py-2 text-sm font-medium rounded-md transition-colors ${
                    isActive
                      ? 'bg-blue-100 text-blue-700 border-r-2 border-blue-600'
                      : 'text-gray-600 hover:bg-gray-50 hover:text-gray-900'
                  }`}
                >
                  <Icon className={`mr-3 h-5 w-5 ${isActive ? 'text-blue-600' : 'text-gray-400'}`} />
                  {item.name}
                </Link>
              );
            })}
          </div>
        </nav>
      </div>

      {/* Main content */}
      <div className="flex-1 flex flex-col overflow-hidden">
        <main className="flex-1 overflow-y-auto">
          <div className="py-6 px-8">
            {children}
          </div>
        </main>
      </div>
    </div>
  );
};