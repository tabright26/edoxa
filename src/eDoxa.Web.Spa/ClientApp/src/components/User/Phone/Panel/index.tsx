import React, { useState, FunctionComponent, useEffect } from "react";
import { Card, CardHeader, CardBody, Button } from "reactstrap";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import Badge from "components/Shared/Badge";
import { connect } from "react-redux";
import UserPhoneForm from "components/User/Phone/Form";
import { compose } from "recompose";
import { Loading } from "components/Shared/Loading";
import { RootState } from "store/types";
import { loadUserPhone } from "store/actions/identity";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Phone: FunctionComponent<any> = ({
  className,
  phone: { data, error, loading },
  loadUserPhone
}) => {
  useEffect((): void => {
    if (data === null) {
      loadUserPhone();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  const [buttonDisabled, setButtonDisabled] = useState(false);
  const disabled = !data || buttonDisabled;
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">PHONE</strong>
        {data && (
          <Badge.Verified className="ml-3 my-auto" verified={data.verified} />
        )}
        <Button
          className="p-0 ml-auto my-auto"
          color="link"
          size="sm"
          disabled={disabled}
          onClick={() => setButtonDisabled(true)}
        >
          <small className="text-uppercase">
            <FontAwesomeIcon icon={faEdit} /> UPDATE
          </small>
        </Button>
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : (
          <dl className="row mb-0">
            <dd className="col-sm-3 text-muted mb-0">Number</dd>
            <dd className="col-sm-5 mb-0">
              {disabled && (
                <UserPhoneForm.Update
                  handleCancel={() => setButtonDisabled(false)}
                />
              )}
              {!disabled && <span>{data.number}</span>}
            </dd>
          </dl>
        )}
      </CardBody>
    </Card>
  );
};

const mapStateToProps = (state: RootState) => {
  return {
    phone: state.root.user.phone
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadUserPhone: () => dispatch(loadUserPhone())
  };
};

const enhance = compose<any, any>(connect(mapStateToProps, mapDispatchToProps));

export default enhance(Phone);
