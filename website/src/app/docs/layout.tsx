'use client';

import React, { useState } from 'react';
import Link from 'next/link';
import Image from 'next/image';
import { usePathname } from 'next/navigation';

export default function DocsLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const pathname = usePathname();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  const navigation = [
    {
      title: 'Getting Started',
      items: [
        { name: 'Introduction', href: '/docs' },
      ],
    },
    {
      title: 'Core Concepts',
      items: [
        { name: 'Plugins & Config', href: '/docs/plugins' },
        { name: 'Commands', href: '/docs/commands' },
        { name: 'Events', href: '/docs/events' },
      ],
    },
    {
      title: 'Services & Drivers',
      items: [
        { name: 'Drivers Registry', href: '/docs/drivers' },
      ],
    },
  ];

  return (
    <div className="min-h-screen bg-white flex flex-col md:flex-row">
      {/* Mobile Header */}
      <header className="md:hidden flex items-center justify-between px-6 py-4 border-b border-zinc-200 bg-white sticky top-0 z-50">
        <Link href="/" className="flex items-center gap-3">
          <Image src="/logo.png" alt="Aero Logo" width={28} height={28} />
          <span className="font-semibold text-zinc-900">Aero Docs</span>
        </Link>
        <button
          onClick={() => setMobileMenuOpen(!mobileMenuOpen)}
          className="p-2 text-zinc-500 hover:text-zinc-900 rounded-lg hover:bg-zinc-100 transition-colors"
        >
          {mobileMenuOpen ? '✕' : '☰'}
        </button>
      </header>

      {/* Sidebar Navigation */}
      <aside className={`
        fixed inset-y-0 left-0 z-40 w-64 bg-zinc-50 border-r border-zinc-200/80 px-6 py-8 flex flex-col justify-between
        transform md:translate-x-0 md:static transition-transform duration-200 ease-in-out
        ${mobileMenuOpen ? 'translate-x-0' : '-translate-x-full'}
      `}>
        <div>
          <div className="mb-8 hidden md:flex items-center gap-3">
            <Link href="/" className="flex items-center gap-3">
              <Image src="/logo.png" alt="Aero Logo" width={32} height={32} />
              <span className="font-bold tracking-tight text-zinc-900">Aero Docs</span>
            </Link>
          </div>

          <nav className="space-y-8">
            {navigation.map((group) => (
              <div key={group.title} className="space-y-3">
                <h3 className="text-xs font-semibold text-zinc-400 uppercase tracking-wider">{group.title}</h3>
                <ul className="space-y-1">
                  {group.items.map((item) => {
                    const isActive = pathname === item.href;
                    return (
                      <li key={item.name}>
                        <Link
                          href={item.href}
                          onClick={() => setMobileMenuOpen(false)}
                          className={`
                            block px-3 py-2 text-sm font-medium rounded-xl transition-all duration-150
                            ${isActive 
                              ? 'bg-zinc-950 text-white shadow-sm' 
                              : 'text-zinc-600 hover:text-zinc-900 hover:bg-zinc-200/50'}
                          `}
                        >
                          {item.name}
                        </Link>
                      </li>
                    );
                  })}
                </ul>
              </div>
            ))}
          </nav>
        </div>

        <div className="pt-6 border-t border-zinc-200/80">
          <Link href="/" className="text-xs font-semibold text-zinc-500 hover:text-zinc-900 transition-colors flex items-center gap-1.5">
            ← Back to Landing Page
          </Link>
        </div>
      </aside>

      {/* Main Content Area */}
      <main className="flex-1 px-6 py-12 md:px-12 lg:px-16 max-w-4xl mx-auto overflow-y-auto w-full">
        <article className="prose prose-zinc max-w-none">
          {children}
        </article>
      </main>
    </div>
  );
}
