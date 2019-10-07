import React, { Fragment } from "react";
import { Badge, Col } from "reactstrap";

const UserInvitationItem = ({ candidature, userId }) => {
  const [doxaTag, setDoxaTag] = useState(null);

  useEffect(() => {
    if (doxaTags) {
      setDoxaTag(doxaTags.find(tag => tag.userId === userId));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [candidature]);

  return (
    <Fragment>
      <Col xs="6" sm="6" md="6">
        <small className="text-muted">{doxaTag ? doxaTag : ""}</small>
      </Col>
      <Col xs="3" sm="3" md="3">
        <Badge color="success">Accept</Badge>
      </Col>
      <Col xs="3" sm="3" md="3">
        <Badge color="danger">Decline</Badge>
      </Col>
    </Fragment>
  );
};

export default UserInvitationItem;
