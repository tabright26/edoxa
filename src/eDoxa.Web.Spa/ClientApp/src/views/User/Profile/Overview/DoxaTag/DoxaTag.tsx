import React, { useState, FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { withUserDoxatag } from "store/root/user/doxatagHistory/container";
import DoxaTagForm from "forms/User/Doxatag";
import { compose } from "recompose";
import Button from "components/Shared/Override/Button";

const DoxaTag: FunctionComponent<any> = ({ className, doxatag, actions }) => {
  const [buttonDisabled, setButtonDisabled] = useState(false);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">DOXATAG</strong>
        <Button.Link className="p-0 ml-auto my-auto" icon={faEdit} disabled={buttonDisabled} onClick={() => setButtonDisabled(true)}>
          UPDATE
        </Button.Link>
      </CardHeader>
      <CardBody>
        <dl className="row mb-0">
          <dd className="col-sm-3 mb-0 text-muted">Doxatag</dd>
          <dd className="col-sm-5 mb-0">
            {!buttonDisabled && doxatag ? (
              <span>
                {doxatag.name}#{doxatag.code}
              </span>
            ) : (
              <DoxaTagForm.Update handleCancel={() => setButtonDisabled(false)} />
            )}
          </dd>
        </dl>
      </CardBody>
    </Card>
  );
};

const enhance = compose<any, any>(withUserDoxatag);

export default enhance(DoxaTag);
