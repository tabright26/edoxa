import React, { useState, FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { withUserDoxatagHistory } from "store/root/user/doxatagHistory/container";
import DoxaTagForm from "forms/User/DoxaTag";
import { compose } from "recompose";

const DoxaTag: FunctionComponent<any> = ({ className, doxaTag, actions }) => {
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
          <dd className="col-sm-5 mb-0">
            {isFormHidden && doxaTag ? (
              <span>
                {doxaTag.name}#{doxaTag.code}
              </span>
            ) : (
              <DoxaTagForm.Update initialValues={doxaTag} onSubmit={data => actions.changeDoxaTag(data).then(() => setFormHidden(true))} handleCancel={() => setFormHidden(true)} />
            )}
          </dd>
        </dl>
      </CardBody>
    </Card>
  );
};

const enhance = compose<any, any>(withUserDoxatagHistory);

export default enhance(DoxaTag);
