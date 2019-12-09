import React, { useState, FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { withUserDoxatag } from "store/root/user/doxatagHistory/container";
import DoxatagForm from "components/User/Doxatag/Form";
import { compose } from "recompose";
import Button from "components/Shared/Button";
import Loading from "components/Shared/Loading";

const Doxatag: FunctionComponent<any> = ({
  className,
  doxatag: { data, error, loading }
}) => {
  const [buttonDisabled, setButtonDisabled] = useState(false);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">DOXATAG</strong>
        <Button.Link
          className="p-0 ml-auto my-auto"
          icon={faEdit}
          disabled={buttonDisabled}
          onClick={() => setButtonDisabled(true)}
        >
          UPDATE
        </Button.Link>
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : (
          <dl className="row mb-0">
            <dd className="col-sm-3 mb-0 text-muted">Doxatag</dd>
            <dd className="col-sm-5 mb-0">
              {!buttonDisabled && data ? (
                <span>
                  {data.name}#{data.code}
                </span>
              ) : (
                <DoxatagForm.Update
                  handleCancel={() => setButtonDisabled(false)}
                />
              )}
            </dd>
          </dl>
        )}
      </CardBody>
    </Card>
  );
};

const enhance = compose<any, any>(withUserDoxatag);

export default enhance(Doxatag);
