import React from "react";

const ListGroup = (props) => {
  const {
    nameProperty,
    valueProperty,
    items,
    selectedItem,
    onItemSelected,
  } = props;

  return (
    <ul className="list-group">
      {items.map((item) => (
        <li
          key={item[valueProperty]}
          className={
            selectedItem === item ? "list-group-item active" : "list-group-item"
          }
          onClick={() => onItemSelected(item)}
          style={{ cursor: "pointer" }}
        >
          {item[nameProperty]}
        </li>
      ))}
    </ul>
  );
};

ListGroup.defaultProps = {
  nameProperty: "name",
  valueProperty: "id",
};

export default ListGroup;
