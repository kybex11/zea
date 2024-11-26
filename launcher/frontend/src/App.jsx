import './App.css';
import { WindowCenter, WindowSetMinSize } from '../wailsjs/runtime/runtime';
import { useState } from 'react';

export default function App() {
    const [isGameInstalled, setIsGameInstalled] = useState<boolean>(false);

    //Window flow base
    WindowCenter();
    WindowSetMinSize(800, 600);

    return (
        <div>
        </div>
    )
}
