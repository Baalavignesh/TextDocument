import { Typography } from "antd";
import React from "react";
import './navbar.styles.css'
import { useNavigate, useParams } from "react-router-dom";

function Navbar() {

    const { Title } = Typography;
    const params = useParams();
    const navigate = useNavigate();

    return (
        <div className="navbar-main">
            <Title level={1} onClick={() => {navigate(`${params.username}`)}} style={{cursor:"pointer"}}>Text Editor</Title>
            <h4></h4>
        </div>
    )
}
export default Navbar;