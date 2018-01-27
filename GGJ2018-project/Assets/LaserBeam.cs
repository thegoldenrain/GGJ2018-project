﻿
	/*
        public int laserDistance = 100; //max raycasting distance
        public int laserLimit = 10; //the laser can be reflected this many times
        public LineRenderer laserRenderer; //the line renderer
        */
void DrawLaser () {
	int laserReflected = 1; //How many times it got reflected
	int vertexCounter = 1; //How many line segments are there
	bool loopActive = true; //Is the reflecting loop active?
	Vector2 laserDirection = transform.up; //direction of the next laser
	Vector2 lastLaserPosition = transform.position; //origin of the next laser

	laserRenderer.SetVertexCount(1);
	laserRenderer.SetPosition(0, transform.position);

	while (loopActive) {
		RaycastHit2D hit = Physics2D.Raycast(lastLaserPosition, laserDirection, laserDistance);

		if (hit) {
			laserReflected++;
			vertexCounter += 3;
			laserRenderer.SetVertexCount (vertexCounter);
			laserRenderer.SetPosition (vertexCounter-3, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
			laserRenderer.SetPosition(vertexCounter-2, hit.point);
			laserRenderer.SetPosition(vertexCounter-1, hit.point);
			lastLaserPosition = hit.point;
			laserDirection = Vector3.Reflect(laserDirection, hit.normal);
		} else {
			laserReflected++;
			vertexCounter++;
			laserRenderer.SetVertexCount (vertexCounter);
			laserRenderer.SetPosition (vertexCounter - 1, lastLaserPosition + (laserDirection.normalized * laserDistance));

			loopActive = false;
		}
		if (laserReflected > laserLimit)
			loopActive = false;
	}
}