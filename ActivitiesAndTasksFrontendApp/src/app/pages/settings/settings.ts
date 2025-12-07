import { Component, signal, OnInit } from '@angular/core';

@Component({
  selector: 'app-settings',
  imports: [],
  templateUrl: './settings.html',
  styleUrl: './settings.css',
})
export class Settings implements OnInit {
  activeSection = signal<string>('profile');

  ngOnInit(): void {
    // Track scroll position to update active section
    window.addEventListener('scroll', () => this.updateActiveSection());
  }

  scrollToSection(sectionId: string): void {
    this.activeSection.set(sectionId);
    const element = document.getElementById(sectionId);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }

  private updateActiveSection(): void {
    const sections = [
      { id: 'profile', offset: 0 },
      { id: 'password', offset: 0 },
      { id: 'notifications', offset: 0 },
      { id: 'appearance', offset: 0 },
      { id: 'language', offset: 0 },
      { id: 'privacy', offset: 0 },
      { id: 'export', offset: 0 },
      { id: 'delete', offset: 0 },
    ];

    for (const section of sections) {
      const element = document.getElementById(section.id);
      if (element) {
        const rect = element.getBoundingClientRect();
        if (rect.top <= 200 && rect.bottom >= 200) {
          this.activeSection.set(section.id);
          break;
        }
      }
    }
  }
}
