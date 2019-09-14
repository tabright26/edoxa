import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faDollarSign } from "@fortawesome/free-solid-svg-icons";
import { faGg } from "@fortawesome/free-brands-svg-icons";

const Balance = balance => {
  console.log(balance);
  return (
    <div className="col-sm-6 accountBalance">
      <small>
        <FontAwesomeIcon icon={faDollarSign} /> Money : {"N/A"}
      </small>
      <br />
      <small>
        <FontAwesomeIcon icon={faGg} /> Token : {"N/A"}
      </small>
    </div>
  );
};

export default Balance;
