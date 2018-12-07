declare partial /matrix {
	set parent_type = /datum;
	set self_math = true;

	public /proc/Add(matrix/Matrix2) -> matrix;
	public /proc/Interpolate(matrix/Matrix2, float/t) -> matrix
	public /proc/Invert() -> matrix;
	public /proc/Scale(float/x, float/y = x) -> matrix;
	public /proc/Subtract(matrix/Matrix2) -> matrix;
	public /proc/Translate(float/x, float/y = x) -> matrix;
	public /proc/Turn(float/angle) -> matrix;
}
