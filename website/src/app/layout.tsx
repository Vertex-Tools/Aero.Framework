import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";

const inter = Inter({
  subsets: ["latin"],
  variable: "--font-inter",
  display: "swap",
});

export const metadata: Metadata = {
  title: "Aero Framework | High-Performance C# for FiveM",
  description: "A high-performance server-side development framework for FiveM. Circular-buffer event dispatcher, dynamic plugin loader, and robust abstractions.",
  icons: {
    icon: "/logo.png",
  }
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" className={`${inter.variable} h-full antialiased`}>
      <body className="min-h-full bg-zinc-50 text-zinc-900 font-sans">{children}</body>
    </html>
  );
}
