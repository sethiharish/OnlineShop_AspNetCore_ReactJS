import React from "react";
import "./modal.css";

const Modal = (props) => {
  const { showModal, data, onHideModal, children: content } = props;
  return showModal ? (
    <div className="modal">
      <div
        className="modal-content"
        style={{ cursor: "zoom-out" }}
        onClick={() => onHideModal(data)}
      >
        {content}
      </div>
    </div>
  ) : null;
};

export default Modal;
