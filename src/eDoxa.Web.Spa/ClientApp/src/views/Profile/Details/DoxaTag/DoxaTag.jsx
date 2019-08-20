import React, { useState } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import withDoxaTagHistory from "../../../../containers/App/User/Profile/Details/withDoxaTagHistory";
import DoxaTagForm from "../../../../forms/Identity/DoxaTag";
const DoxaTagCard = ({ className, doxaTag }) => {
  const [isFormHidden, setFormHidden] = useState(true);
  return (
    <Card className={className}>
      <CardHeader>
        <strong>DOXATAG</strong>
        {isFormHidden ? (
          <div className="card-header-actions btn-link" onClick={() => setFormHidden(false)}>
            <small>
              <FontAwesomeIcon icon={faEdit} /> UPDATE
            </small>
          </div>
        ) : null}
      </CardHeader>
      <CardBody>
        <dl className="row mb-0">
          <dd className="col-sm-3 mb-0 text-muted">DoxaTag</dd>
          <dd className="col-sm-9 mb-0">
            {isFormHidden ? (
              <span>
                {doxaTag.name}#{doxaTag.code}
              </span>
            ) : (
              <DoxaTagForm.Update handleCancel={() => setFormHidden(true)} />
            )}
          </dd>
        </dl>
      </CardBody>
    </Card>
  );
};

export default withDoxaTagHistory(DoxaTagCard);
